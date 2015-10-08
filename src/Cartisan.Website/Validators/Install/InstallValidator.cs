using Cartisan.Website.Models.Install;
using FluentValidation;
using FluentValidation.Attributes;

namespace Cartisan.Website.Validators.Install {
    public class InstallValidator: AbstractValidator<InstallModel> {
        public InstallValidator() {
            RuleFor(x => x.AdminEmail).NotEmpty().WithMessage("请输入管理员邮箱");
            RuleFor(x => x.AdminEmail).EmailAddress();
            RuleFor(x => x.AdminPassword).NotEmpty().WithMessage("请输入管理员密码");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("请输入确认密码");
            RuleFor(x => x.AdminPassword).Equal(x => x.ConfirmPassword).WithMessage("密码不匹配");
            RuleFor(x => x.DataProvider).NotEmpty().WithMessage("选择数据提供程序");
        }
    }
}