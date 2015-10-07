using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;

namespace Cartisan.Web.Data {
    /// <summary>
    /// 管理数据设置（连接字符串）
    /// </summary>
    public class DataSettingsManager {
        public const char  Separator = ':';
        public const string Filename = "Settings.txt";

        /// <summary>
        /// 读取设置
        /// </summary>
        /// <param name="filePath">文件路径。如果是null，则使用默认设置文件路径</param>
        /// <returns></returns>
        public virtual DataSettings LoadSettings(string filePath=null) {
            if(string.IsNullOrEmpty(filePath)) {
                // 使用webHelper.MapMath代替HostingEnvironment.MapPath在单元测试中是不可用的。
                filePath = Path.Combine(MapPath("~/App_Data/"), Filename);
            }

            if(File.Exists(filePath)) {
                string text = File.ReadAllText(filePath);

                return ParseSettings(text);
            }

            return new DataSettings();
        }

        /// <summary>
        /// 保存设置到文件中
        /// </summary>
        /// <param name="settings"></param>
        public virtual void SaveSettings(DataSettings settings) {
            AssertionConcern.ArgumentNotNull(settings, "settings不能为空。");

            // 使用webHelper.MapMath代替HostingEnvironment.MapPath在单元测试中是不可用的。
            string filePath = Path.Combine(MapPath("~/App_Data/"), Filename);

            if(!File.Exists(filePath)) {
                using(File.Create(filePath)) {
                    // 使用“using”使文件创建后关闭。
                }
            }

            var text = ComposeSettings(settings);
            File.WriteAllText(filePath, text);
        }

        /// <summary>
        /// 映射虚拟路径到物理磁盘路径
        /// </summary>
        /// <param name="path">虚拟路径。如："~/bin"</param>
        /// <returns>物理路径。如："c:\inetpub\wwwroot\bin"</returns>
        protected virtual string MapPath(string path) {
            if(HostingEnvironment.IsHosted) {
                // 宿主
                return HostingEnvironment.MapPath(path);
            }

            // 非宿主，用于运行单元测试示例
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        /// <summary>
        /// 解释设置
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private DataSettings ParseSettings(string text) {
            DataSettings shellSettings = new DataSettings();
            if(string.IsNullOrEmpty(text)) {
                return shellSettings;
            }

            var settings = new List<string>();
            using(StringReader reader = new StringReader(text)) {
                string str;
                while((str=reader.ReadLine())!=null) {
                    settings.Add(str);
                }
            }

            foreach(string setting in settings) {
                int separatorIndex = setting.IndexOf(Separator);
                if(separatorIndex==-1) {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                switch(key) {
                    case "DataProvider":
                        shellSettings.DataProvider = value;
                        break;
                    case "DataConnectionString":
                        shellSettings.DataConnectionString = value;
                        break;
                    default:
                        shellSettings.RawDataSettings.Add(key,value);
                        break;
                }
            }

            return shellSettings;
        }

        /// <summary>
        /// 把设置数据转换成字符串表示形式
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        private string ComposeSettings(DataSettings settings) {
            if(settings==null) {
                return "";
            }

            return string.Format("DataProvider: {0}{2}DataConnectionString: {1}{2}",
                settings.DataProvider, settings.DataConnectionString, Environment.NewLine);
        }
    }
}