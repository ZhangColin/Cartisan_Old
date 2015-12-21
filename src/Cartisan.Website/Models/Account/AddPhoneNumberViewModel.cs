using System.ComponentModel.DataAnnotations;

namespace Cartisan.Website.Models.Account {
    public class AddPhoneNumberViewModel {
        [Required]
        [Phone]
        [Display(Name = "电话号码")]
        public string Number { get; set; }
    }
}