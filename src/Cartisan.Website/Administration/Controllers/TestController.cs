using System;
using System.Web.Mvc;
using Cartisan.Caching;
using Cartisan.DependencyInjection;
using Cartisan.Logging;

namespace Cartisan.Admin.Controllers {
    public class TestController : Controller {
        public void Log() {
            var logger = ServiceLocator.GetService<ILoggerFactory>().CreateLogger("Cartisan");
            logger.Debug("Cartisan - Debug");
            logger.Info("Cartisan - Info");
            logger.Warn("Cartisan - Warn");
            logger.Error("Cartisan - Error");
            logger.Fatal("Cartisan - Fatal");

            logger = ServiceLocator.GetService<ILoggerFactory>().CreateLogger(this.GetType());
            logger.Debug(this.GetType().Name + " - Debug");
            logger.Info(this.GetType().Name + " - Info");
            logger.Warn(this.GetType().Name + " - Warn");
            logger.Error(this.GetType().Name + " - Error");
            logger.Fatal(this.GetType().Name + " - Fatal");
        }

        public string Cache() {
            ICacheService cacheService = ServiceLocator.GetService<ICacheService>();
            var cacheEntry = cacheService.Get<CacheEntry>("cacheentry", () => new CacheEntry(), new TimeSpan(0, 0, 5));

            return cacheEntry.Count.ToString();
        }
    }

    public class CacheEntry {
        private static int _count;

        public CacheEntry() {
            _count++;
        }

        public int Count {
            get {
                return _count;
            }
        }
    }
}