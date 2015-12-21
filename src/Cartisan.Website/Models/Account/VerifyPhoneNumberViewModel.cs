using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class VerifyPhoneNumberViewModel {
        [Required]
        [Display(Name = "����")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "�绰����")]
        public string PhoneNumber { get; set; }
    }
}