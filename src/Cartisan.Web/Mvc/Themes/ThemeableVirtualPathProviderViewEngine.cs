//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//
//namespace Cartisan.Web.Mvc.Themes {
//    public abstract class ThemeableVirtualPathProviderViewEngine: VirtualPathProviderViewEngine {
//        // format is ":ViewCacheEntry:{cacheType}:{prefix}:{name}:{controllerName}:{areaName}:{theme}:"
//        private const string _cacheKeyFormat = ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}:";
//        private const string _cacheKeyPrefixMaster = "Master";
//        private const string _cacheKeyPrefixPartial = "Partial";
//        private const string _cacheKeyPrefixView = "View";
//        private static readonly string[] _emptyLocations = new string[0];
//
//        protected Func<string, string> GetExtensionThunk = VirtualPathUtility.GetExtension;
//
//
//
//        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName,
//            string masterName, bool useCache) {
//            if(controllerContext == null) {
//                throw new ArgumentNullException("controllerContext");
//            }
//            if(string.IsNullOrEmpty(viewName)) {
//                throw new ArgumentException("值不能为空。", "viewName");
//            }
//
//            return this.FindThemeableView(controllerContext, viewName, masterName, useCache);
//        }
//
//        protected virtual ViewEngineResult FindThemeableView(ControllerContext controllerContext, string viewName,
//            string masterName, bool useCache) {
//            string theme = string.Empty;
//
//            string[] viewLocationsSearched;
//            string[] masterLocationsSearched;
//
//            string controllerName = controllerContext.RouteData.GetRequiredString("controller");
//            string viewPath = this.GetPath(controllerContext, this.ViewLocationFormats, this.AreaViewLocationFormats,
//                "ViewLocationFormats", viewName, controllerName, theme, _cacheKeyPrefixView, useCache, out viewLocationsSearched);
//            string masterPath = this.GetPath(controllerContext, this.MasterLocationFormats, this.AreaMasterLocationFormats,
//                "MasterLocationFormats", masterName, controllerName, theme, _cacheKeyPrefixMaster, useCache,
//                out masterLocationsSearched);
//
//            if (string.IsNullOrEmpty(viewPath) || (string.IsNullOrEmpty(masterPath) && !string.IsNullOrEmpty(masterName))) {
//                return new ViewEngineResult(viewLocationsSearched.Union(masterLocationsSearched));
//            }
//            return new ViewEngineResult(this.CreateView(controllerContext, viewPath, masterPath), this);
//        }
//
//        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName,
//            bool useCache) {
//            if(controllerContext == null) {
//                throw new ArgumentNullException("controllerContext");
//            }
//            if(string.IsNullOrEmpty(partialViewName)) {
//                throw new ArgumentException("值不能为空。", "partialViewName");
//            }
//
//            return this.FindThemeablePartialView(controllerContext, partialViewName, useCache);
//        }
//
//        protected virtual ViewEngineResult FindThemeablePartialView(ControllerContext controllerContext,
//            string partialViewName, bool useCache) {
//            string theme = string.Empty;
//
//            string[] searched;
//            string controllerName = controllerContext.RouteData.GetRequiredString("controller");
//            string partialPath = this.GetPath(controllerContext, this.PartialViewLocationFormats,
//                this.AreaPartialViewLocationFormats, "PartialViewLocationFormats", partialViewName, controllerName,
//                theme, _cacheKeyPrefixPartial, useCache, out searched);
//
//            if (string.IsNullOrEmpty(partialPath)) {
//                return new ViewEngineResult(searched);
//            }
//
//            return new ViewEngineResult(this.CreatePartialView(controllerContext, partialPath), this);
//        }
//
//        public abstract IDictionary<string, string[]> SpecificAreaViewLocationFormats { get; } 
//        public abstract IDictionary<string, string[]> SpecificAreaMasterLocationFormats { get; }
//        public abstract IDictionary<string, string[]> SpecificAreaPartialViewLocationFormats { get; } 
//
//        private string GetPath(ControllerContext controllerContext, string[] viewLocationFormats,
//            string[] areaViewLocationFormats, string locationsPropertyName, string viewName, string controllerName,
//            string theme, string cacheKeyPrefix, bool useCache, out string[] searchedLocations) {
//            searchedLocations = _emptyLocations;
//
//            if(string.IsNullOrEmpty(viewName)) {
//                return string.Empty;
//            }
//
//            string areaName = AreaHelpers.GetAreaName(controllerContext.RouteData);
//            bool usingAreas = !string.IsNullOrEmpty(areaName);
//
//            if(usingAreas) {
//                string[] specificLocations = new string[0];
//                if(locationsPropertyName == "ViewLocationFormats" &&
//                    this.SpecificAreaViewLocationFormats.Keys.Any(
//                        key => key.Equals(areaName, StringComparison.InvariantCultureIgnoreCase))) {
//                    specificLocations =
//                        this.SpecificAreaViewLocationFormats[
//                            this.SpecificAreaViewLocationFormats.Keys.First(
//                                key => key.Equals(areaName, StringComparison.InvariantCultureIgnoreCase))];
//                }
//                else if(locationsPropertyName == "MasterLocationFormats" &&
//                    this.SpecificAreaMasterLocationFormats.Keys.Any(
//                        key => key.Equals(areaName, StringComparison.InvariantCultureIgnoreCase))) {
//                            specificLocations =
//                                this.SpecificAreaMasterLocationFormats[
//                                    this.SpecificAreaMasterLocationFormats.Keys.First(
//                                        key => key.Equals(areaName, StringComparison.InvariantCultureIgnoreCase))];
//                }
//                else if(locationsPropertyName == "PartialViewLocationFormats" &&
//                    this.SpecificAreaPartialViewLocationFormats.Keys.Any(
//                        key => key.Equals(areaName, StringComparison.InvariantCultureIgnoreCase))) {
//                            specificLocations =
//                                this.SpecificAreaPartialViewLocationFormats[
//                                    this.SpecificAreaPartialViewLocationFormats.Keys.First(
//                                        key => key.Equals(areaName, StringComparison.InvariantCultureIgnoreCase))];
//                }
//
//                viewLocationFormats = specificLocations.Union(viewLocationFormats).ToArray();
//            }
//
//            List<ViewLocation> viewLocations = GetViewLocations(viewLocationFormats, usingAreas ? areaViewLocationFormats : null);
//
//            if(viewLocations.Count==0) {
//                throw new InvalidOperationException(string.Format("属性 {0} 不能为空。", locationsPropertyName));
//            }
//
//            bool nameRepresentsPath = IsSpecificPath(viewName);
//
//            string cacheKey = this.CreateCacheKey(cacheKeyPrefix, viewName,
//                nameRepresentsPath ? string.Empty : controllerName, areaName, theme);
//
//            if(useCache) {
////                IEnumerable<IDisplayMode> possibleDisplayModes =
////                    DisplayModeProvider.GetAvailableDisplayModesForContext(controllerContext.HttpContext,
////                        controllerContext.DisplayMode);
////                foreach(var displayMode in possibleDisplayModes) {
////                    string cachedLocation = ViewLocationCache.GetViewLocation(controllerContext.HttpContext,
////                        AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId));
////                    if(cachedLocation==null) {
////                        return null;
////                    }
////
////                    if(cachedLocation.Length>0) {
////                        if(controllerContext.DisplayMode==null) {
////                            controllerContext.DisplayMode = displayMode;
////                        }
////
////                        return cachedLocation;
////                    }
////                }
////
////                return null;
//                return this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, cacheKey);
//            }
//            else {
//                return nameRepresentsPath
//                    ? this.GetPathFromSpecificName(controllerContext, viewName, cacheKey, ref searchedLocations)
//                    : this.GetPathFromGeneralName(controllerContext, viewLocations, viewName,
//                        controllerName, areaName, theme, cacheKey, ref searchedLocations);
//            }
//        }
//
//        private string GetPathFromGeneralName(ControllerContext controllerContext, List<ViewLocation> locations,
//            string viewName, string controllerName, string areaName, string theme, string cacheKey,
//            ref string[] searchedLocations) {
//            string result = string.Empty;
//            searchedLocations = new string[locations.Count];
//            for(int i = 0; i < locations.Count; i++) {
//                ViewLocation location = locations[i];
//                string virtualPath = location.Format(viewName, controllerName, areaName, theme);
//                if(this.FileExists(controllerContext, virtualPath)) {
//                    searchedLocations = _emptyLocations;
//                    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
//                    result = virtualPath;
//                }
//                else {
//                    searchedLocations[i] = virtualPath;
//                }
////                DisplayInfo virtualPathDisplayInfo = DisplayModeProvider.GetDisplayInfoForVirtualPath(virtualPath,
////                    controllerContext.HttpContext, path => this.FileExists(controllerContext, path),
////                    controllerContext.DisplayMode);
////
////                if(virtualPathDisplayInfo != null) {
////                    string resolvedVirtualPath = virtualPathDisplayInfo.FilePath;
////
////                    searchedLocations = _emptyLocations;
////                    result = resolvedVirtualPath;
////                    ViewLocationCache.InsertViewLocation(controllerContext.HttpContext,
////                        AppendDisplayModeToCacheKey(cacheKey, virtualPathDisplayInfo.DisplayMode.DisplayModeId), result);
////
////                    if(controllerContext.DisplayMode == null) {
////                        controllerContext.DisplayMode = virtualPathDisplayInfo.DisplayMode;
////                    }
////                    IEnumerable<IDisplayMode> allDisplayModes = DisplayModeProvider.Modes;
////                    foreach(var displayMode in allDisplayModes) {
////                        if(displayMode.DisplayModeId != virtualPathDisplayInfo.DisplayMode.DisplayModeId) {
////                            DisplayInfo displayInfoToCache = displayMode.GetDisplayInfo(controllerContext.HttpContext,
////                                virtualPath, path => this.FileExists(controllerContext, path));
////                            string cacheValue = string.Empty;
////                            if(displayInfoToCache != null && displayInfoToCache.FilePath != null) {
////                                cacheValue = displayInfoToCache.FilePath;
////                            }
////                            ViewLocationCache.InsertViewLocation(controllerContext.HttpContext,
////                                AppendDisplayModeToCacheKey(cacheKey, displayMode.DisplayModeId), cacheValue);
////                        }
////                    }
////                    break;
////                }
////                else {
////                    searchedLocations[i] = virtualPath;
////                }
//            }
//
//            return result;
//        }
//
//        private string GetPathFromSpecificName(ControllerContext controllerContext, string viewName, 
//            string cacheKey, ref string[] searchedLocations) {
//            string result = viewName;
//
//            if(!(this.FilePathIsSupported(viewName) && this.FileExists(controllerContext, viewName))) {
//                result = string.Empty;
//                searchedLocations = new[] {viewName};
//            }
//
//            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, result);
//            return result;
//        }
//
//        private bool FilePathIsSupported(string virtualPath) {
//            if(this.FileExtensions==null) {
//                return true;
//            }
//            else {
//                string extension = this.GetExtensionThunk(virtualPath).TrimStart('.');
//                return this.FileExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
//            }
//        }
//
//        private static string AppendDisplayModeToCacheKey(string cacheKey, string displayMode) {
//            return cacheKey + displayMode + ":";
//        }
//
//        protected virtual string CreateCacheKey(string prefix, string viewName, string controllerName, string areaName,
//            string theme) {
//            return string.Format(CultureInfo.InvariantCulture, _cacheKeyFormat, this.GetType().AssemblyQualifiedName,
//                prefix, viewName, controllerName, areaName, theme);
//        }
//
//        private static bool IsSpecificPath(string name) {
//            char c = name[0];
//            return (c == '~' || c == '/');
//        }
//
//        private static List<ViewLocation> GetViewLocations(string[] viewLocationFormats,
//            string[] areaViewLocationFormats) {
//            List<ViewLocation> allLocations = new List<ViewLocation>();
//
//            if(areaViewLocationFormats!=null) {
//                allLocations.AddRange(
//                    areaViewLocationFormats.Select(
//                        areaViewLocationFormat => new AreaAwareViewLocation(areaViewLocationFormat)));
//            }
//
//            if(viewLocationFormats!=null) {
//                allLocations.AddRange(
//                    viewLocationFormats.Select(viewLocationFormat => new ViewLocation(viewLocationFormat)));
//            }
//
//            return allLocations;
//        } 
//
//        private class AreaAwareViewLocation: ViewLocation {
//            public AreaAwareViewLocation(string virtualPathFormatString): base(virtualPathFormatString) {}
//
//            public override string Format(string viewName, string controllerName, string areaName, string theme) {
//                return string.Format(CultureInfo.InvariantCulture, this._virtualPathFormatString, viewName, controllerName,
//                    areaName, theme);
//            }
//        }
//
//        private class ViewLocation {
//            protected string _virtualPathFormatString;
//            public ViewLocation(string virtualPathFormatString) {
//                this._virtualPathFormatString = virtualPathFormatString;
//            }
//
//            public virtual string Format(string viewName, string controllerName, string areaName, string theme) {
//                return string.Format(CultureInfo.InvariantCulture, this._virtualPathFormatString, viewName, controllerName, 
//                    theme);
//            }
//        }
//    }
//}