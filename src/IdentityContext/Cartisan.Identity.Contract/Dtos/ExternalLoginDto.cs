using System.Collections.Generic;

namespace Cartisan.Identity.Contract.Dtos {
    public class ExternalLoginInfo {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ManageInfoDto {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<AccountLoginInfo> Logins { get; set; }

        public IEnumerable<ExternalLoginInfo> ExternalLoginProviders { get; set; }
    }

    public class AccountInfo {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class AccountLoginInfo {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}