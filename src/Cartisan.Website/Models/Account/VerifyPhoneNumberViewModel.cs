using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class VerifyPhoneNumberViewModel {
        [Required]
        [Display(Name = "´úÂë")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "µç»°ºÅÂë")]
        public string PhoneNumber { get; set; }
    }
}