namespace Cartisan.Web.Data {
    public class DataSettingsHelper {
        private static bool? _databaseIsInstalled;

        public static bool DatabaseIsInstalled() {
            var manager = new DataSettingsManager();
            var settings = manager.LoadSettings();

            _databaseIsInstalled = settings != null && !string.IsNullOrEmpty(settings.DataConnectionString);
            return _databaseIsInstalled.Value;
        }

        public static void ResetCache() {
            _databaseIsInstalled = null;
        }
    }
}