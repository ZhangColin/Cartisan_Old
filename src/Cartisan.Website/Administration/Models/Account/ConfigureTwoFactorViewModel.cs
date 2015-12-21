using System.Collections.Generic;

namespace Cartisan.Admin.Models.Account {
    public class ConfigureTwoFactorViewModel {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}