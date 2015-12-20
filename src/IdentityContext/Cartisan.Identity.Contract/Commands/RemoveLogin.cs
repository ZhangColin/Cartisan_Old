namespace Cartisan.Identity.Contract.Commands {
    public class RemoveLogin {
        //        [Required]
        //        [Display(Name = "登录提供程序")]
        public string LoginProvider { get; set; }

        //        [Required]
        //        [Display(Name = "提供程序密钥")]
        public string ProviderKey { get; set; }
    }
}