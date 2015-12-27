using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class LoginViewModel {
        [Required]
        public string Username { get; set; }

        [Required]
        [Display(Name = "电子邮件")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }
    }
}