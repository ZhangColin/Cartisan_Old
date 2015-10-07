using System.Collections.Generic;
using System.Web.Mvc;

namespace Cartisan.Web.Mvc.Themes {
    public class ThemeableRazorViewEngine: ThemeableVirtualPathProviderViewEngine {
        public ThemeableRazorViewEngine() {
            AreaViewLocationFormats = new[]
                                          {
                                              //themes
                                              "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                                              "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",
                                              
                                              //default
                                              "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                              "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                          };

            AreaMasterLocationFormats = new[]
                                            {
                                                //themes
                                                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                                                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",


                                                //default
                                                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                            };

            AreaPartialViewLocationFormats = new[]
                                                 {
                                                     //themes
                                                    "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
                                                    "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",
                                                    
                                                    //default
                                                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                                                 };

            ViewLocationFormats = new[]
                                      {
                                            //themes
                                            "~/Themes/{2}/Views/{1}/{0}.cshtml",
                                            "~/Themes/{2}/Views/Shared/{0}.cshtml",

                                            //default
                                            "~/Views/{1}/{0}.cshtml",
                                            "~/Views/Shared/{0}.cshtml",

                                            //Admin
                                            "~/Administration/Views/{1}/{0}.cshtml",
                                            "~/Administration/Views/Shared/{0}.cshtml",
                                      };

            MasterLocationFormats = new[]
                                        {
                                            //themes
                                            "~/Themes/{2}/Views/{1}/{0}.cshtml",
                                            "~/Themes/{2}/Views/Shared/{0}.cshtml", 

                                            //default
                                            "~/Views/{1}/{0}.cshtml",
                                            "~/Views/Shared/{0}.cshtml"
                                        };

            PartialViewLocationFormats = new[]
                                             {
                                                 //themes
                                                "~/Themes/{2}/Views/{1}/{0}.cshtml",
                                                "~/Themes/{2}/Views/Shared/{0}.cshtml",

                                                //default
                                                "~/Views/{1}/{0}.cshtml",
                                                "~/Views/Shared/{0}.cshtml", 

                                                //Admin
                                                "~/Administration/Views/{1}/{0}.cshtml",
                                                "~/Administration/Views/Shared/{0}.cshtml",
                                             };

            FileExtensions = new[] { "cshtml" };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, null, false, fileExtensions);
            //return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions, base.ViewPageActivator);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, masterPath, true, fileExtensions);
        }
    }
}


//using System.Web.Mvc;
//
//namespace Cartisan.Web.Mvc.Themes {
//    public class ThemeableRazorViewEngine: VirtualPathProviderViewEngine {
//        public ThemeableRazorViewEngine() {
//            this.AreaViewLocationFormats = new[] {
//                // themes
//                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
//                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.vbhtml",
//                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",
//                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.vbhtml",
//
//                // default
//                "~/Areas/{2}/Views/{1}/{0}.cshtml",
//                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
//                "~/Areas/{2}/Views/Shared/{0}.cshtml",
//                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
//            };
//
//            this.AreaMasterLocationFormats = new[] {
//                // themes
//                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
//                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.vbhtml",
//                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",
//                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.vbhtml",
//
//                // default
//                "~/Areas/{2}/Views/{1}/{0}.cshtml",
//                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
//                "~/Areas/{2}/Views/Shared/{0}.cshtml",
//                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
//            };
//
//            this.AreaPartialViewLocationFormats = new[] {
//                // themes
//                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.cshtml",
//                "~/Areas/{2}/Themes/{3}/Views/{1}/{0}.vbhtml",
//                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.cshtml",
//                "~/Areas/{2}/Themes/{3}/Views/Shared/{0}.vbhtml",
//
//                // default
//                "~/Areas/{2}/Views/{1}/{0}.cshtml",
//                "~/Areas/{2}/Views/{1}/{0}.vbhtml",
//                "~/Areas/{2}/Views/Shared/{0}.cshtml",
//                "~/Areas/{2}/Views/Shared/{0}.vbhtml"
//            };
//
//            this.ViewLocationFormats = new[] {
//                // themes
//                "~/Themes/{2}/Views/{1}/{0}.cshtml",
//                "~/Themes/{2}/Views/{1}/{0}.vbhtml",
//                "~/Themes/{2}/Views/Shared/{0}.cshtml",
//                "~/Themes/{2}/Views/Shared/{0}.vbhtml",
//
//                // Admin
//                "~/Administration/Views/{1}/{0}.cshtml",
//                "~/Administration/Views/{1}/{0}.vbhtml",
//                "~/Administration/Views/Shared/{0}.cshtml",
//                "~/Administration/Views/Shared/{0}.vbhtml",
//                
//                // default
//                "~/Views/{1}/{0}.cshtml",
//                "~/Views/{1}/{0}.vbhtml",
//                "~/Views/Shared/{0}.cshtml",
//                "~/Views/Shared/{0}.vbhtml"
//            };
//
//            this.MasterLocationFormats = new[] {
//                // themes
//                "~/Themes/{2}/Views/{1}/{0}.cshtml",
//                "~/Themes/{2}/Views/{1}/{0}.vbhtml",
//                "~/Themes/{2}/Views/Shared/{0}.cshtml",
//                "~/Themes/{2}/Views/Shared/{0}.vbhtml",
//
//                // Admin
//                "~/Administration/Views/{1}/{0}.cshtml",
//                "~/Administration/Views/{1}/{0}.vbhtml",
//                "~/Administration/Views/Shared/{0}.cshtml",
//                "~/Administration/Views/Shared/{0}.vbhtml",
//
//                // default
//                "~/Views/{1}/{0}.cshtml",
//                "~/Views/{1}/{0}.vbhtml",
//                "~/Views/Shared/{0}.cshtml",
//                "~/Views/Shared/{0}.vbhtml"
//            };
//
//            this.PartialViewLocationFormats = new[] {
//                // themes
//                "~/Themes/{2}/Views/{1}/{0}.cshtml",
//                "~/Themes/{2}/Views/{1}/{0}.vbhtml",
//                "~/Themes/{2}/Views/Shared/{0}.cshtml",
//                "~/Themes/{2}/Views/Shared/{0}.vbhtml",
//
//                // Admin
//                "~/Administration/Views/{1}/{0}.cshtml",
//                "~/Administration/Views/{1}/{0}.vbhtml",
//                "~/Administration/Views/Shared/{0}.cshtml",
//                "~/Administration/Views/Shared/{0}.vbhtml",
//
//                // default
//                "~/Views/{1}/{0}.cshtml",
//                "~/Views/{1}/{0}.vbhtml",
//                "~/Views/Shared/{0}.cshtml",
//                "~/Views/Shared/{0}.vbhtml"
//            };
//
//            this.FileExtensions = new[] {"cshtml", "vbhtml"};
//        }
//
//        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
//            string layoutPath = null;
//            bool runViewStartPages = false;
//            string[] fileExtensions = this.FileExtensions;
//
//            return new RazorView(controllerContext, partialPath, layoutPath, runViewStartPages, fileExtensions);
//        }
//
//        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
//            string layoutPath = masterPath;
//            bool runViewStartPages = true;
//            string[] fileExtensions = this.FileExtensions;
//
//            return new RazorView(controllerContext, viewPath, layoutPath, runViewStartPages, fileExtensions);
//        }
//
//        
//
////        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache) {
////            var areaName = AreaHelpers.GetAreaName(controllerContext.RouteData);
////            if(!string.IsNullOrEmpty(areaName) && 
////                areaName.Equals("admin", StringComparison.InvariantCultureIgnoreCase)) {
////                var controllerName = controllerContext.RouteData.GetRequiredString("controller");
////                var viewPaths = new[] {
////                    string.Format("~/Administration/Views/{1}/{0}.cshtml", viewName, controllerName),
////                    string.Format("~/Administration/Views/Shared/{0}.cshtml", viewName)
////                }.Where(path => this.FileExists(controllerContext, path));
////                var viewPath = viewPaths != null && viewPaths.Count() > 0 ? viewPaths.ToList()[0] : "";
////                
////                var masterPaths = new[] {
////                    string.Format("~/Administration/Views/{1}/{0}.cshtml", masterName, controllerName),
////                    string.Format("~/Administration/Views/Shared/{0}.cshtml", masterName)
////                }.Where(path => this.FileExists(controllerContext, path));
////                var masterPath = masterPaths != null && masterPaths.Count() > 0 ? masterPaths.ToList()[0] : "";
////
////                return new ViewEngineResult(this.CreateView(controllerContext, viewPath, masterPath), this);
////            }
////            
////            return base.FindView(controllerContext, viewName, masterName, useCache);
////        }
////
////        protected override bool FileExists(ControllerContext controllerContext, string virtualPath) {
////            return BuildManager.GetObjectFactory(virtualPath, false) != null;
////        }
////
////        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache) {
////            string areaName = string.Empty;
////            object value;
////            if (controllerContext.RouteData.DataTokens.TryGetValue("area", out value)) {
////                areaName = value as string;
////            }
////            else {
////                var area = controllerContext.RouteData.Route as IRouteWithArea;
////                if (area != null) {
////                    areaName = area.Area;
////                }
////                else {
////                    var route = controllerContext.RouteData.Route as Route;
////                    if (route != null && route.DataTokens != null) {
////                        if (route.DataTokens.TryGetValue("area", out value)) {
////                            areaName = value as string;
////                        }
////                    }
////                }
////            }
////            if (!string.IsNullOrEmpty(areaName) &&
////                areaName.Equals("admin", StringComparison.InvariantCultureIgnoreCase)) {
////                var controllerName = controllerContext.RouteData.GetRequiredString("controller");
////                var virtualPath = new[] {
////                    string.Format("~/Administration/Views/{1}/{0}.cshtml", partialViewName, controllerName),
////                    string.Format("~/Administration/Views/Shared/{0}.cshtml", partialViewName)
////                }.Where(path => this.FileExists(controllerContext, path));
////
////                return new ViewEngineResult(this.CreatePartialView(controllerContext, virtualPath.ToList()[0]), this);
////            }
////
////            return base.FindPartialView(controllerContext, partialViewName, useCache);
////        }
//    }
//}