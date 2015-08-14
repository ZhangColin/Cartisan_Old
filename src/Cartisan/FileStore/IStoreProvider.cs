using System;
using System.Collections.Generic;
using System.IO;

namespace Cartisan.FileStore {
    /// <summary>
    /// 文件存储提供器
    /// </summary>
    public interface IStoreProvider {
        /// <summary>
        /// 文件存储根路径
        /// </summary>
        string StoreRootPath { get; }

        /// <summary>
        /// 直连URL根路径
        /// </summary>
        string DirectlyRootUrl { get; }

        /// <summary>
        /// 通过相对路径及文件名称获取文件
        /// </summary>
        /// <param name="relativePath">相对路径（如：000\111\222）</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        IStoreFile GetFile(string relativePath, string fileName);

        /// <summary>
        /// 通过文件相对路径获取文件
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（如：000\111\222\2.jpg）</param>
        /// <returns></returns>
        IStoreFile GetFile(string relativeFileName);

        /// <summary>
        /// 获取文件路径中的所有文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="isOnlyCurrentFolder">是否只获取当前层次的文件</param>
        /// <returns></returns>
        IEnumerable<IStoreFile> GetFiles(string relativePath, bool isOnlyCurrentFolder);


        /// <summary>
        /// 创建或更新一个文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="contentStream">文件内容流</param>
        /// <returns></returns>
        IStoreFile AddOrUpdateFile(string relativePath, string fileName, Stream contentStream);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        void DeleteFile(string relativePath, string fileName);

        /// <summary>
        /// 删除以指定前缀开头的文件
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        void DeleteFiles(string relativePath, string fileNamePrefix);

        /// <summary>
        /// 删除文件目录
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        void DeleteFolder(string relativePath);

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        string GetDirectlyUrl(string relativePath, string fileName);

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <param name="lastModified">最后修改时间</param>
        /// <returns></returns>
        string GetDirectlyUrl(string relativePath, string fileName, DateTime lastModified);

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（包括文件名）</param>
        /// <returns></returns>
        string GetDirectlyUrl(string relativeFileName);

        /// <summary>
        /// 获取文件直连URL
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（包括文件名）</param>
        /// <param name="lastModified">最后修改时间</param>
        /// <returns></returns>
        string GetDirectlyUrl(string relativeFileName, DateTime lastModified);

        /// <summary>
        /// 从本地物理路径获取相对路径
        /// </summary>
        /// <param name="fullLocalPath">本地物理路径</param>
        /// <param name="pathIncludesFileName">是否包含文件名</param>
        /// <returns></returns>
        string GetRelativePath(string fullLocalPath, bool pathIncludesFileName);

        /// <summary>
        /// 获取本地物理路径
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        string GetFullLocalPath(string relativePath, string fileName);

        /// <summary>
        /// 获取本地物理路径
        /// </summary>
        /// <param name="relativeFileName">文件相对路径（包括文件名）</param>
        /// <returns></returns>
        string GetFullLocalPath(string relativeFileName);

        /// <summary>
        /// 连接路径
        /// </summary>
        /// <param name="directoryParts"></param>
        /// <returns></returns>
        string JoinDirectory(params string[] directoryParts);
    }
}