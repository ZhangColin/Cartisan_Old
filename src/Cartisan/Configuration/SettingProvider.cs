//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Reflection;
//using Cartisan.Dapper;
//using Cartisan.Infrastructure;
//
//namespace Cartisan.Configuration {
//    public class SettingProvider: ISettingProvider {
//        private const string _tableName = "Settings";
//
//        protected virtual void InsertSetting(Setting setting) {
//            AssertionConcern.NotNull(setting, "设置不能为空。");
//
//            Database.Insert(setting, _tableName);
//        }
//        
//        protected virtual void UpdateSetting(Setting setting) {
//            AssertionConcern.NotNull(setting, "设置不能为空。");
//
//            Database.Update(setting, new {setting.Id}, _tableName);
//        }
//
//        protected virtual void DeleteSetting(Setting setting) {
//            AssertionConcern.NotNull(setting, "设置不能为空。");
//
//            Database.Delete(new { setting.Id }, _tableName);
//        }
//
//        protected Setting GetSettingById(int settingId) {
//            return Database.QueryList<Setting>(new { id = settingId }, _tableName).FirstOrDefault();
//        }
//
//        protected virtual IDictionary<string, Setting> GetAllSettingsCached() {
//            IEnumerable<Setting> settings = Database.QueryList<Setting>(null, _tableName);
//
//            return settings.ToDictionary(s => s.Name, s => s);
//        }
//
//        public IList<Setting> GetAllSettings() {
//            return Database.QueryList<Setting>(null, _tableName).OrderBy(s=>s.Name).ToList();
//        }
//
//        public T GetSettingByKey<T>(string key, T defaultValue = default(T)) {
//            if(string.IsNullOrEmpty(key)) {
//                return defaultValue;
//            }
//
//            var settings = this.GetAllSettingsCached();
//            if(settings.ContainsKey(key)) {
//                if(settings[key]!=null) {
//                    return CommonHelper.To<T>(settings[key].Value);
//                }
//            }
//
//            return defaultValue;
//        }
//
//        public void SetSetting<T>(string key, T value, bool clearCache = true) {
//            AssertionConcern.NotNull(key, "key");
//            string valueStr = CommonHelper.GetCustomTypeConverter(typeof(T)).ConvertToInvariantString(value);
//
//            var allSettings = this.GetAllSettingsCached();
//            if(allSettings.ContainsKey(key)) {
//                Setting setting = allSettings[key];
//                setting.Value = valueStr;
//                this.UpdateSetting(setting);
//            }
//            else {
//                this.InsertSetting(new Setting(key, valueStr));
//            }
//        }
//
//        public bool SettingExists<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T: ISettings, new() {
//            string key = GeneratorKey(keySelector);
//
//            return this.GetSettingByKey<string>(key) != null;
//        }
//
//        public T LoadSetting<T>() where T: ISettings, new() {
//            T settings = Activator.CreateInstance<T>();
//
//            foreach(var property in typeof(T).GetProperties()) {
//                if (!property.CanRead || !property.CanWrite) {
//                    continue;
//                }
//
//                string key = GeneratorKey<T>(property);
//                string setting = this.GetSettingByKey<string>(key);
//                if(setting==null) {
//                    continue;
//                }
//
//                if(!CommonHelper.GetCustomTypeConverter(property.PropertyType).CanConvertFrom(typeof(string))) {
//                    continue;
//                }
//
//                if(!CommonHelper.GetCustomTypeConverter(property.PropertyType).IsValid(setting)) {
//                    continue;
//                }
//
//                object value =
//                    CommonHelper.GetCustomTypeConverter(property.PropertyType).ConvertFromInvariantString(setting);
//
//                property.SetValue(settings, value, null);
//            }
//
//            return settings;
//        }
//
//        public void SaveSetting<T>(T settings) where T: ISettings, new() {
//            foreach(var property in typeof(T).GetProperties()) {
//                if(!property.CanRead || !property.CanWrite) {
//                    continue;
//                }
//
//                if(!CommonHelper.GetCustomTypeConverter(property.PropertyType).CanConvertFrom(typeof(string))) {
//                    continue;
//                }
//
//                string key = GeneratorKey<T>(property);
//
//                dynamic value = property.GetValue(settings, null);
//
//                if(value!=null) {
//                    this.SetSetting(key, value, false);
//                }
//                else {
//                    this.SetSetting(key, "", false);
//                }
//            }
//        }
//
//        public void DeleteSetting<T>() where T: ISettings, new() {
//            var allSettings = this.GetAllSettings();
//
//            foreach(var property in typeof(T).GetProperties()) {
//                string key = GeneratorKey<T>(property);
//                Setting setting = allSettings.FirstOrDefault(s => s.Name == key);
//
//                if(setting!=null) {
//                    DeleteSetting(setting);
//                }
//            }
//        }
//
//        public void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T: ISettings, new() {
//            var key = GeneratorKey(keySelector);
//
//            var allSettings = this.GetAllSettingsCached();
//            if(allSettings.ContainsKey(key)) {
//                DeleteSetting(allSettings[key]);
//            }
//        }
//
//        private static string GeneratorKey<T>(PropertyInfo property) where T: ISettings, new() {
//            return (typeof(T).Name + "." + property.Name).ToLowerInvariant();
//        }
//
//        private static string GeneratorKey<T, TPropType>(Expression<Func<T, TPropType>> keySelector) where T: ISettings, new() {
//            var member = keySelector.Body as MemberExpression;
//            AssertionConcern.NotNull(member,
//                string.Format("Expression '{0}' refers to a method, not a property.", keySelector));
//
//            var propInfo = member.Member as PropertyInfo;
//            AssertionConcern.NotNull(propInfo,
//                string.Format("Expression '{0}' refers to a field, not a property.", keySelector));
//
//            string key = GeneratorKey<T>(propInfo);
//            return key;
//        }
//
//        public void ClearCache() {
//            throw new NotImplementedException();
//        }
//    }
//}