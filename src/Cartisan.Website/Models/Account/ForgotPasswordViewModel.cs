using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class ForgotPasswordViewModel {
        [Required]
        [EmailAddress]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
}