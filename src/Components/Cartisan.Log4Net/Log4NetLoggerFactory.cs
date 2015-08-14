//using System;
//using System.IO;
//using Cartisan.Log4Net;
//using log4net;
//using log4net.Config;
//
//namespace Cartisan.Logging.Log4Net {
//    public class Log4NetLoggerFactory: ILoggerFactory {
//        private static bool _isConfigLoaded;
//
//        public Log4NetLoggerFactory()
//            : this("~/Config/log4net.config") {
//        }
//
//        public Log4NetLoggerFactory(string configFileName) {
//            if(_isConfigLoaded) {
//                return;
//            }
//
//            if(string.IsNullOrEmpty(configFileName)) {
//                configFileName = "~/Config/log4net.config";
//            }
//
//            FileInfo configFile = new FileInfo(HostEnvironment.MapPath(configFileName));
//            if(!configFile.Exists) {
//                throw new ApplicationException(string.Format("log4net配置文件 {0} 未找到。", configFile.FullName));
//            }
//
//            if(HostEnvironment.IsFullTrust()) {
//                XmlConfigurator.ConfigureAndWatch(configFile);
//            }
//            else {
//                XmlConfigurator.Configure(configFile);
//            }
//
//            _isConfigLoaded = true;
//        }
//
//        public ILogger CreateLogger(string name) {
//            return new Log4NetLogger(LogManager.GetLogger(name));
//        }
//
//        public ILogger CreateLogger(Type type) {
//            return new Log4NetLogger(LogManager.GetLogger(type));
//        }
//    }
//}