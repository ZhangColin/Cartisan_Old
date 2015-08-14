using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Cartisan;

namespace Cartisan.Web {
    /// <summary>
    /// 运行环境
    /// </summary>
    public static class HostEnvironment {
        private const string WebConfigPath = "~/web.config";
        private const string RefreshHtmlPath = "~/refresh.html";
        private const string HostRestartPath = "~/bin/HostRestart";

        /// <summary>
        /// 是否是完全信任的运行环境
        /// </summary>
        /// <returns></returns>
        public static bool IsFullTrust() {
            return AppDomain.CurrentDomain.IsHomogenous && AppDomain.CurrentDomain.IsFullyTrusted;
        }

        /// <summary>
        /// 重新启动AppDomain
        /// </summary>
        public static void RestartAppDomain() {
            if (IsFullTrust()) {
                HttpRuntime.UnloadAppDomain();
            }
            else {
                bool success = TryWriteBinFolder() || TryWriteWebConfig();

                if (!success) {
                    throw new CartisanException(
                        "在非FullTrust环境下，不支持 UnloadAppDomain ，需要重启站点，必须给 ~/bin 或 ~/web.config 写权限。");
                }
            }

            //            HttpContext httpContext = HttpContext.Current;
            //
            //            if (httpContext!=null) {
            //                if (httpContext.Request.RequestType=="GET") {
            //                    httpContext.Response.Redirect(httpContext.Request.ToUrlString(), true /* endResponse*/);
            //                }
            //                else {
            //                    httpContext.Response.ContentType = "text/html";
            //                    httpContext.Response.WriteFile(RefreshHtmlPath);
            //                    httpContext.Response.End();
            //                }
            //            }
        }

        /// <summary>
        /// 尝试修改 web.config 的最后更新时间使应用程序自动重新加载
        /// </summary>
        /// <returns>修改成功返回 true, 否则返回 false</returns>
        private static bool TryWriteWebConfig() {
            try {
                File.SetLastWriteTimeUtc(MapPath(WebConfigPath), DateTime.UtcNow);
                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 尝试引起 bin 文件夹的改动使应用程序自动重新加载
        /// </summary>
        /// <returns>成功写入返回 true，否则返回 false</returns>
        private static bool TryWriteBinFolder() {
            try {
                string binMarker = MapPath(HostRestartPath);
                Directory.CreateDirectory(binMarker);

                using (var stream = File.CreateText(Path.Combine(binMarker, "marker.txt"))) {
                    stream.WriteLine("系统重启于： {0}", DateTime.UtcNow);
                    stream.Flush();
                }

                return true;
            }
            catch {
                return false;
            }
        }

        /// <summary>
        /// 程序集是否已经加载
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsAssemblyLoaded(string name) {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Any(assembly => new AssemblyName(assembly.FullName).Name == name);
        }

        /// <summary>
        /// 将虚拟路径映射到服务器上的物理路径
        /// </summary>
        /// <param name="virtualPath">虚拟路径</param>
        /// <returns>物理路径</returns>
        public static string MapPath(string virtualPath) {
            return HostingEnvironment.MapPath(virtualPath);
        }
    }
}