using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class LoginViewModel {
        [Required(ErrorMessage = "请填写用户名/邮箱/手机")]
        public string Username { get; set; }

//        [Required(ErrorMessage = "请填写邮箱")]
//        [Display(Name = "电子邮件1")]
//        [EmailAddress]
//        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
        
        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}