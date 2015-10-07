using System.Collections.Generic;

namespace Cartisan.Web.Data {
    /// <summary>
    /// 数据库设置(连接字符串信息)
    /// </summary>
    public class DataSettings {
        public DataSettings() {
            RawDataSettings = new Dictionary<string, string>();
        }

        /// <summary>
        /// 数据提供器
        /// </summary>
        public string DataProvider { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DataConnectionString { get; set; }

        /// <summary>
        /// 原始设置文件
        /// </summary>
        public IDictionary<string, string> RawDataSettings { get; private set; }

        /// <summary>
        /// 输入的数据是否有效
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return !string.IsNullOrEmpty(this.DataProvider)
                && !string.IsNullOrEmpty(this.DataConnectionString);
        }
    }
}