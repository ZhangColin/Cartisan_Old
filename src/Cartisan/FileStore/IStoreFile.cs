using System;
using System.IO;

namespace Cartisan.FileStore {
    /// <summary>
    /// 存储的文件
    /// </summary>
    public interface IStoreFile {
        /// <summary>
        /// 文件名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 扩展名
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// 文件大小
        /// </summary>
        long Size { get; }

        /// <summary>
        /// 相对于存储路径（StoragePath）的路径
        /// </summary>
        string RelativePath { get; }

        /// <summary>
        /// 完整的文件物理路径
        /// </summary>
        string FullLocalPath { get; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime LastModified { get; }

        /// <summary>
        /// 获取文件流
        /// </summary>
        /// <returns></returns>
        Stream OpenReadStream();
    }
}