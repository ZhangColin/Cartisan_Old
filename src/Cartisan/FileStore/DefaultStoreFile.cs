using System;
using System.IO;

namespace Cartisan.FileStore {
    /// <summary>
    /// 存储文件默认实现
    /// </summary>
    public class DefaultStoreFile: IStoreFile {
        private readonly string _relativePath;
        private readonly FileInfo _fileInfo;
        private readonly string _fullLocalPath;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileInfo">文件对象</param>
        public DefaultStoreFile(string relativePath, FileInfo fileInfo) {
            _relativePath = relativePath;
            _fileInfo = fileInfo;
            _fullLocalPath = _fileInfo.FullName;
        }

        /// <summary>
        /// 文件名
        /// </summary>
        public string Name {
            get { return _fileInfo.Name; }
        }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extension {
            get { return _fileInfo.Extension; }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size {
            get { return _fileInfo.Length; }
        }

        /// <summary>
        /// 相对于存储路径（StoragePath）的路径
        /// </summary>
        public string RelativePath {
            get { return this._relativePath; }
        }

        /// <summary>
        /// 完整的文件物理路径
        /// </summary>
        public string FullLocalPath {
            get { return _fullLocalPath; }
        }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastModified {
            get { return this._fileInfo.LastWriteTime; }
        }

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <returns></returns>
        public Stream OpenReadStream() {
            return new FileStream(FullLocalPath, FileMode.Open, FileAccess.Read);
        }
    }
}