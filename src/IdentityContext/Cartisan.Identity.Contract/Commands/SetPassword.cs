namespace Cartisan.Identity.Contract.Commands {
    public class SetPassword {
        //        [Required]
        //        [StringLength(100, ErrorMessage = "{0} �������ٰ��� {2} ���ַ���", MinimumLength = 6)]
        //        [DataType(DataType.Password)]
        //        [Display(Name = "������")]
        public string NewPassword { get; set; }

        //        [DataType(DataType.Password)]
        //        [Display(Name = "ȷ��������")]
        //        [Compare("NewPassword", ErrorMessage = "�������ȷ�����벻ƥ�䡣")]
        public string ConfirmPassword { get; set; }
    }
}