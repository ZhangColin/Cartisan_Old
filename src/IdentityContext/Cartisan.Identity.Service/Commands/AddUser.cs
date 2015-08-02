using Cartisan.CommandProcessor;

namespace Cartisan.Identity.Service.Commands {
    public class AddUser: ICommand {
        public string Name { get; set; }
    }
}