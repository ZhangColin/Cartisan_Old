using System.ComponentModel.DataAnnotations;

namespace Cartisan.Admin.Models.Account {
    public class VerifyPhoneNumberViewModel {
        [Required]
        [Display(Name = "代码")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "电话号码")]
        public string PhoneNumber { get; set; }
    }
}