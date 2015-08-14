using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Cartisan.Web.Utility {
    /// <summary>
    /// 提供与Web请求时可使用的工具类，包括Url解析、Url/Html编码、获取IP地址、返回http状态码
    /// </summary>
    public static class WebUtility {
        /// <summary>
        /// 将URL转换为在请求客户端可用的URL（转换 ~/ 为绝对路径）
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        public static string ResolveUrl(string relativeUrl) {
            if (string.IsNullOrEmpty(relativeUrl) || !relativeUrl.StartsWith("~/")) {
                return relativeUrl;
            }

            string[] sections = relativeUrl.Split('?');
            string url = VirtualPathUtility.ToAbsolute(sections[0]);
            if (sections.Length > 1) {
                url = url + "?" + sections[1];
            }

            return url;
        }

        /// <summary>
        /// 获取物理文件路径
        /// </summary>
        /// <param name="filePath">
        /// filePath支持以下格式：
        ///     ~/abc/
        ///     c:\abc\
        ///     \\192.168.0.1\abc\
        /// </param>
        /// <returns></returns>
        public static string GetPhysicalFilePath(string filePath) {
            if (filePath.IndexOf(":\\") != -1 || filePath.IndexOf("\\\\") != -1) {
                return filePath;
            }
            else if (HostingEnvironment.IsHosted) {
                return HostingEnvironment.MapPath(filePath);
            }
            else {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    filePath.Replace('/', Path.DirectorySeparatorChar).Replace("~", ""));
            }
        }
    }
}