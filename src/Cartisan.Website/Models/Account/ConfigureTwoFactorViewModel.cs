using System.Collections.Generic;

namespace Cartisan.Website.Models.Account {
    public class ConfigureTwoFactorViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}