using System;
using System.Configuration;
using System.Data.SqlClient;
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

        [HttpPost]
        public ActionResult Index(InstallModel model) {
            if (DataSettingsHelper.DatabaseIsInstalled()) {
                return RedirectToRoute("HomePage");
            }

            // 设置页面超时为5分钟
            this.Server.ScriptTimeout = 300;

            if(model.DatabaseConnectionString!=null) {
                model.DatabaseConnectionString = model.DatabaseConnectionString.Trim();
            }

            model.DisableSqlCompact =
                !string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseFastInstallationService"]) &&
                    Convert.ToBoolean(ConfigurationManager.AppSettings["UseFastInstallationService"]);
            model.DisableSampleDataOption =
                !string.IsNullOrEmpty(ConfigurationManager.AppSettings["DisableSampleDataDuringInstallation"]) &&
                    Convert.ToBoolean(ConfigurationManager.AppSettings["DisableSampleDataDuringInstallation"]);

            if(model.DataProvider.Equals("sqlserver", StringComparison.InvariantCultureIgnoreCase)) {
                if (model.SqlConnectionInfo.Equals("sqlconnectioninfo_raw", StringComparison.InvariantCultureIgnoreCase)) {
                    if(string.IsNullOrEmpty(model.DatabaseConnectionString)) {
                        ModelState.AddModelError("", "连接字符串是必填的");

                        try {
                            // 尝试创建连接字符串
                            new SqlConnectionStringBuilder(model.DatabaseConnectionString);
                        }
                        catch(Exception) {
                            ModelState.AddModelError("", "连接字符串的格式错误");
                        }
                    }
                }
                else {
                    if (string.IsNullOrEmpty(model.SqlServerName)) {
                        ModelState.AddModelError("", "请输入SQL Server 实例名");
                    }
                    if (string.IsNullOrEmpty(model.SqlDatabaseName)) {
                        ModelState.AddModelError("", "请输入数据库名称");
                    }

                    // 授权方式
                    if(model.SqlAuthenticationType.Equals("sqlauthentication", StringComparison.InvariantCultureIgnoreCase)) {
                        // SQL授权
                        if (string.IsNullOrEmpty(model.SqlServerUsername)) {
                            ModelState.AddModelError("", "请输入SQL Server用户名");
                        }
                        if (string.IsNullOrEmpty(model.SqlServerPassword)) {
                            ModelState.AddModelError("", "请输入SQL Server密码");
                        }
                    }
                }
            }



            if (ModelState.IsValid) {
                var settingsManager = new DataSettingsManager();
                try {
                    string connectionString;
                    if(model.) {
                        
                    }
                }
                catch(Exception) {
                    
                    throw;
                }
            }

            return View(model);
        }

        /// <summary>
        /// 检查是否存在指定的数据库，如果数据库存在，返回true
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <returns>如果数据库存在，返回true</returns>
        [NonAction]
        private bool SqlServerDatabaseExists(string connectionString) {
            try {
                // 尝试连接
                using(var conn = new SqlConnection(connectionString)) {
                    conn.Open();
                }

                return true;
            }
            catch(Exception) {
                return false;
            }
        }

        /// <summary>
        /// 在服务器上创建数据库
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="collation">自定义的Sql Server排序规则，如果未指定使用默认的</param>
        /// <returns>错误信息</returns>
        [NonAction]
        private string CreateDatabase(string connectionString, string collation) {
            try {
                //解析数据库名称
                var builder = new SqlConnectionStringBuilder(connectionString);
                var databaseName = builder.InitialCatalog;

                // 创建到连接到master库的连接字符串，master是永远存在的
                builder.InitialCatalog = "master";
                var masterCatalogConnectionString = builder.ToString();
                string query = string.Format("CREATE DATABASE [{0}]", databaseName);
                if(!string.IsNullOrWhiteSpace(collation)) {
                    query = string.Format("{0} COLLATE {1}", query, collation);
                }
                using(var conn = new SqlConnection(masterCatalogConnectionString)) {
                    conn.Open();
                    using(var command = new SqlCommand(query, conn)) {
                        command.ExecuteNonQuery();
                    }
                }

                return string.Empty;
            }
            catch(Exception ex) {
                return string.Format("创建数据库: {0}的时候发生错误", ex.Message);
            }
        }

        /// <summary>
        /// 创建SqlConnection所使用的连接字符串
        /// </summary>
        /// <param name="trustedConnection">是否使用Windows账户进行身份验证</param>
        /// <param name="serverName">连接到SQL Server的实例的名称或网络地址</param>
        /// <param name="databaseName">与连接相关联的数据库的名称</param>
        /// <param name="userName">连接到SQL Server使用的用户ID</param>
        /// <param name="password">SQL Server的帐户密码</param>
        /// <param name="timeout">连接超时</param>
        /// <returns>连接字符串</returns>
        [NonAction]
        private string CreateConnectionString(bool trustedConnection, string serverName, string databaseName,
            string userName, string password, int timeout = 0) {
            var builder = new SqlConnectionStringBuilder();
            builder.IntegratedSecurity = trustedConnection;
            builder.DataSource = serverName;
            builder.InitialCatalog = databaseName;
            if(!trustedConnection) {
                builder.UserID = userName;
                builder.Password = password;
            }
            builder.PersistSecurityInfo = false;
            builder.MultipleActiveResultSets = false;
            if(timeout>0) {
                builder.ConnectTimeout = timeout;
            }
            return builder.ConnectionString;
        }
    }
}