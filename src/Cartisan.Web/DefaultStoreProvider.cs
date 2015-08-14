using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Cartisan.Extensions;
using Cartisan.FileStore;
using Cartisan.FileStore.Network;
using Cartisan.Web.Utility;

namespace Cartisan.Web {
    /// <summary>
    /// 默认文件存储提供器
    /// </summary>
    public class DefaultStoreProvider: IStoreProvider {
        private string _storeRootPath;
        private string _directlyRootUrl;
        private static readonly Regex ValidPathPattern;
        private static readonly Regex ValidFileNamePattern;
        private NetworkShareAccesser _accesser;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DefaultStoreProvider() {
            StringBuilder validFileNamePatternBuilder = new StringBuilder();
            validFileNamePatternBuilder.Append("^[^");
            foreach (char c in Path.GetInvalidFileNameChars()) {
                validFileNamePatternBuilder.Append(Regex.Escape(new string(c, 1)));
            }
            validFileNamePatternBuilder.Append("]{1,255}$");
            ValidFileNamePattern = new Regex(validFileNamePatternBuilder.ToString(),
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);

            StringBuilder validPathPatternBuilder = new StringBuilder();
            validPathPatternBuilder.Append("^[^");
            foreach (char c in Path.GetInvalidPathChars()) {
                validPathPatternBuilder.Append(Regex.Escape(new string(c, 1)));
            }
            validPathPatternBuilder.Append("]{0,769}$");
            ValidPathPattern = new Regex(validPathPatternBuilder.ToString(),
                RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="storeRootPath"></param>
        public DefaultStoreProvider(string storeRootPath) : this(storeRootPath, WebUtility.ResolveUrl(storeRootPath)) {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="storeRootPath"></param>
        /// <param name="directlyRootUrl"></param>
        public DefaultStoreProvider(string storeRootPath, string directlyRootUrl) {
            this._storeRootPath = WebUtility.GetPhysicalFilePath(storeRootPath);
            if (!string.IsNullOrEmpty(this.StoreRootPath)) {
                this._storeRootPath = this.StoreRootPath.TrimEnd('/', '\\');
            }

            this._directlyRootUrl = directlyRootUrl;
            if (!string.IsNullOrEmpty(this.DirectlyRootUrl)) {
                this._directlyRootUrl = WebUtility.ResolveUrl(this.DirectlyRootUrl.TrimEnd('/', '\\'));
            }
        }

        /// <summary>
        /// 构造函数（适用于访问UNC地址）
        /// </summary>
        /// <param name="storeRootPath"></param>
        /// <param name="directlyRootUrl"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public DefaultStoreProvider(string storeRootPath, string directlyRootUrl, string userName, string password): 
        this(storeRootPath,directlyRootUrl){
            _accesser = new NetworkShareAccesser(storeRootPath, userName, password);
            _accesser.Disconnect();
            _accesser.Connect();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DefaultStoreProvider() {
            if (_accesser!=null) {
                _accesser.Disconnect();
            }
        }

        /// <summary>
        /// 该Provider文件存储根路径，支持虚拟目录、应用程序根（~/）、（/）、UNC路径、文件系统物理路径
        /// </summary>
        public string StoreRootPath {
            get { return this._storeRootPath; }
        }

        /// <summary>
        /// 直连URL根路径
        /// </summary>
        public string DirectlyRootUrl {
            get { return this._directlyRootUrl; }
        }

        /// <summary>
        /// 通过相对路径及文件名称获取文件
        /// </summary>
        /// <param name="relativePath">相对路径（如：000\111\222）</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public IStoreFile GetFile(string relativePath, string fileName) {
            string fullLocalPath = this.GetFullLocalPath(relativePath, fileName);
            if (File.Exists(fullLocalPath)) {
                return new DefaultStoreFile(relativePath, new FileInfo(fullLocalPath));
            }

            return null;
        }

        /// <summary>
        /// 通过文件相对路径获取文件
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（如：000\111\222\2.jpg）</param>
        /// <returns></returns>
        public IStoreFile GetFile(string relativeFileName) {
            string fullLocalPath = this.GetFullLocalPath(relativeFileName);
            string relativePath = this.GetRelativePath(fullLocalPath, true);
            if (File.Exists(fullLocalPath)) {
                return new DefaultStoreFile(relativePath, new FileInfo(fullLocalPath));
            }

            return null;
        }

        /// <summary>
        /// 获取文件路径中的所有文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="isOnlyCurrentFolder">是否只获取当前层次的文件</param>
        /// <returns></returns>
        public IEnumerable<IStoreFile> GetFiles(string relativePath, bool isOnlyCurrentFolder) {
            if (!IsValidPath(relativePath)) {
                throw new CartisanException("路径不合法。");
            }
            string fullLocalPath = this.GetFullLocalPath(relativePath);
            if (Directory.Exists(fullLocalPath)) {
                return new DirectoryInfo(fullLocalPath)
                    .GetFiles("*.*", isOnlyCurrentFolder ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories)
                    .Where(fileInfo => (fileInfo.Attributes & FileAttributes.Hidden) != FileAttributes.Hidden)
                    .Select(fileInfo => new DefaultStoreFile(
                        isOnlyCurrentFolder ? relativePath : GetRelativePath(fileInfo.FullName, true), fileInfo));

            }
            return Enumerable.Empty<IStoreFile>();
        }

        /// <summary>
        /// 创建或更新一个文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentStream">文件内容流</param>
        /// <returns></returns>
        public IStoreFile AddOrUpdateFile(string relativePath, string fileName, Stream contentStream) {
            if (contentStream==null || !contentStream.CanRead) {
                throw new CartisanException("文件流为空或文件流不可读。");
            }
            if (!IsValidPathAndFileName(relativePath, fileName)) {
                throw new CartisanException("路径或文件名不合法");
            }

            EnsurePathExists(GetFullLocalPath(relativePath));
            string fullLocalPath = this.GetFullLocalPath(relativePath, fileName);

            using (FileStream fileStream = File.OpenWrite(fullLocalPath)) {
                byte[] buffer = new byte[contentStream.Length > 65536L ? 65536 : contentStream.Length];
                int count;
                while ((count=contentStream.Read(buffer, 0, buffer.Length))>0) {
                    fileStream.Write(buffer, 0, count);
                }
                fileStream.Flush();
                fileStream.Close();
            }

            return new DefaultStoreFile(relativePath, new FileInfo(fullLocalPath));
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        public void DeleteFile(string relativePath, string fileName) {
            if (!IsValidPathAndFileName(relativePath, fileName)) {
                throw new CartisanException("路径或文件名不合法");
            }
            string fullLocalPath = this.GetFullLocalPath(relativePath, fileName);
            if (!File.Exists(fullLocalPath)) {
                throw new CartisanException("文件不存在。");
            }
            File.Delete(fullLocalPath);
        }

        /// <summary>
        /// 删除以指定前缀开头的文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        public void DeleteFiles(string relativePath, string fileNamePrefix) {
            if (!IsValidPath(relativePath)) {
                throw new CartisanException("路径不合法。");
            }
            string fullLocalPath = GetFullLocalPath(relativePath);
            if (!Directory.Exists(fullLocalPath)) {
                throw new CartisanException("目录不存在。");
            }

            new DirectoryInfo(fullLocalPath).GetFiles(fileNamePrefix + "*").ForEach(file => file.Delete());
        }

        /// <summary>
        /// 删除文件目录
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        public void DeleteFolder(string relativePath) {
            if (!IsValidPath(relativePath)) {
                throw new CartisanException("路径不合法。");
            }
            string fullLocalPath = GetFullLocalPath(relativePath);
            if (!Directory.Exists(fullLocalPath)) {
                throw new CartisanException("目录不存在。");
            }
            Directory.Delete(fullLocalPath, true);
        }

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public string GetDirectlyUrl(string relativePath, string fileName) {
            return GetDirectlyUrl(relativePath, fileName, DateTime.MinValue);
        }

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="lastModified">最后修改时间</param>
        /// <returns></returns>
        public string GetDirectlyUrl(string relativePath, string fileName, DateTime lastModified) {
            if (string.IsNullOrEmpty(relativePath)) {
                return GetDirectlyUrl(fileName, lastModified);
            }
            if (relativePath.EndsWith("\\") || relativePath.EndsWith("/")) {
                return this.GetDirectlyUrl(relativePath + fileName, lastModified);
            }
            return this.GetDirectlyUrl(relativePath + "/" + fileName, lastModified);
        }

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（包括文件名）</param>
        /// <returns></returns>
        public string GetDirectlyUrl(string relativeFileName) {
            return GetDirectlyUrl(relativeFileName, DateTime.MinValue);
        }

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（包括文件名）</param>
        /// <param name="lastModified">最后修改时间</param>
        /// <returns></returns>
        public string GetDirectlyUrl(string relativeFileName, DateTime lastModified) {
            if (string.IsNullOrEmpty(this.DirectlyRootUrl)) {
                return string.Empty;
            }
            relativeFileName = relativeFileName.Replace("\\", "/");
            return string.Join("",
                this.DirectlyRootUrl,
                relativeFileName.StartsWith("/") ? "" : "/",
                relativeFileName,
                lastModified == DateTime.MinValue ? "" : "?lm=" + lastModified.Ticks);
        }

        /// <summary>
        /// 从本地物理路径获取相对路径
        /// </summary>
        /// <param name="fullLocalPath">本地物理路径</param>
        /// <param name="pathIncludesFileName">是否包含文件名</param>
        /// <returns></returns>
        public string GetRelativePath(string fullLocalPath, bool pathIncludesFileName) {
            return (pathIncludesFileName
                ? fullLocalPath.Substring(0, fullLocalPath.LastIndexOf(Path.DirectorySeparatorChar))
                : fullLocalPath)
                .Replace(StoreRootPath, string.Empty).Trim(Path.DirectorySeparatorChar);
        }

        /// <summary>
        /// 获取本地物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public string GetFullLocalPath(string relativePath, string fileName) {
            string path = GetFullLocalPath(relativePath);
            if (!string.IsNullOrEmpty(fileName)) {
                path = Path.Combine(path, fileName);
            }

            return path;
        }

        /// <summary>
        /// 获取本地物理路径
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（包括文件名）</param>
        /// <returns></returns>
        public string GetFullLocalPath(string relativeFileName) {
            string path = this.StoreRootPath;
            if (path.EndsWith(":")) {
                path += "\\";
            }
            if (!string.IsNullOrEmpty(relativeFileName)) {
                path = Path.Combine(path, relativeFileName);
            }

            return path;
        }

        /// <summary>
        /// 连接路径
        /// </summary>
        /// <param name="directoryParts"></param>
        /// <returns></returns>
        public string JoinDirectory(params string[] directoryParts) {
            return string.Join(new string(Path.DirectorySeparatorChar, 1), directoryParts);
        }

        /// <summary>
        /// 验证文件路径是否合法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static bool IsValidPath(string path) {
            return ValidPathPattern.IsMatch(path);
        }

        /// <summary>
        /// 验证文件名是否合法
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool IsValidFileName(string fileName) {
            return ValidFileNamePattern.IsMatch(fileName);
        }

        /// <summary>
        /// 验证路径与文件名是否合法
        /// </summary>
        /// <param name="path"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool IsValidPathAndFileName(string path, string fileName) {
            if (IsValidPath(path) && IsValidFileName(fileName)) {
                return Encoding.UTF8.GetBytes(path + new string(Path.DirectorySeparatorChar, 1) + fileName)
                    .Length <= 1024;
            }
            return false;
        }

        /// <summary>
        /// 确保文件目录存在
        /// </summary>
        /// <param name="fullLocalPath"></param>
        private static void EnsurePathExists(string fullLocalPath) {
            if (!Directory.Exists(fullLocalPath)) {
                Directory.CreateDirectory(fullLocalPath);
            }
        }
    }
}