using System.Web;

namespace Cartisan.Web {
    public static class CookiePersister {
        public static string GetStringFromCookie(string key) {
            return GetObjectFromCookie(key).ToString();
        }

        public static object GetObjectFromCookie(string key) {
            return HttpContext.Current.Response.Cookies[key];
        }

        public static void SetItemInCookie(string item, string key) {
            var cookie = new HttpCookie(key, item);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void ClearItemFromCookie(string key) {
            HttpContext.Current.Response.Cookies.Remove(key);
        }
    }
}                 