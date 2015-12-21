using System.ComponentModel.DataAnnotations;

namespace Cartisan.Admin.Models.Account {
    public class AddPhoneNumberViewModel {
        [Required]
        [Phone]
        [Display(Name = "电话号码")]
        public string Number { get; set; }
    }
}