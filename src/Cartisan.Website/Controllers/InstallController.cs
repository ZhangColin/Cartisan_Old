using System;
using System.Configuration;
using System.Web.Mvc;
using Cartisan.Web.Data;
using Cartisan.Website.Models.Install;

namespace Cartisan.Website.Controllers {
    public class InstallController: Controller {
        public InstallController() {}

        public ActionResult Index() {
            if(DataSettingsHelper.DatabaseIsInstalled()) {
                return RedirectToRoute("HomePage");
            }

            // 设置页面超时为5分钟
            this.Server.ScriptTimeout = 300;

            var model = new InstallModel() {
                AdminEmail = "admin@cartisan.net.cn",
                InstallSampleData = false,
                DatabaseConnectionString = "",
                DataProvider = "sqlserver",
                //快速安装服务不支持SQL compact
                DisableSqlCompact = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseFastInstallationService"]) &&
                    Convert.ToBoolean(ConfigurationManager.AppSettings["UseFastInstallationService"]),
                DisableSampleDataOption = !string.IsNullOrEmpty(ConfigurationManager.AppSettings["DisableSampleDataDuringInstallation"]) &&
                    Convert.ToBoolean(ConfigurationManager.AppSettings["DisableSampleDataDuringInstallation"]),
                SqlAuthenticationType = "sqlauthentication",
                SqlConnectionInfo = "sqlconnectioninfo_values",
                SqlServerCreateDatabase = false,
                UseCustomCollation = false,
                Collation = "SQL_Latin1_General_CP1_CI_AS"
            };

            return View(model);
        }
    }
}