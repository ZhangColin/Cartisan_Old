namespace Cartisan.WebHost.Models {
    public class CreateAccountViewModel {
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string TrueName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public bool UsingRandomPassword { get; set; }
        public bool NextLoginNeedModifyPassword { get; set; }
        public bool SendActivationEmail { get; set; }
    }
}