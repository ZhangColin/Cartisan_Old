using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class ForgotViewModel {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }
}