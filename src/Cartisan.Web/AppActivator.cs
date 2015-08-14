using Cartisan.Web.Mvc;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Cartisan.Web.AppActivator), "PreStart")]
[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Cartisan.Web.AppActivator), "PostStart")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Cartisan.Web.AppActivator), "Stop")]
namespace Cartisan.Web {
    public static class AppActivator {
        public static void PreStart() {
            // Code that runs before Application_Start.
            ValueProviderConfig.Initialize();
        }
        public static void PostStart() {
            // Code that runs after Application_Start.
        }
        public static void Stop() {
        }
    }
}