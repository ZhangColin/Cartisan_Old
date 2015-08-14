using Cartisan.CommandProcessor;
using Cartisan.Identity.Domain.Models;
using Cartisan.Identity.Service.Commands;
using Cartisan.Repository;

namespace Cartisan.Identity.Service {
    public class AccountProcessor: ICommandHandler<AddUser>, ICommandHandler<UpdateUser>, ICommandHandler<DeleteUser> {
        private readonly IRepository<UserAccount> _userRepository;
        public AccountProcessor(IRepository<UserAccount> userRepository) {
            _userRepository = userRepository;
        }

        public void Execute(AddUser command) {
            _userRepository.Add(new UserAccount() {
                Name = command.Name
            });
        }

        public void Execute(UpdateUser command) {
            var userAccount = _userRepository.Get(command.UserId);
            userAccount.Name = command.Name;
            
            _userRepository.Save(userAccount);
        }

        public void Execute(DeleteUser command) {
            _userRepository.Remove(command.UserId);
        }
    }
}