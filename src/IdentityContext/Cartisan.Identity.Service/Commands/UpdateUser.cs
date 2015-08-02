using System;
using Cartisan.CommandProcessor;

namespace Cartisan.Identity.Service.Commands {
    public class UpdateUser: ICommand {
        public Guid UserId { get; set; }
        public string Name { get; set; }
    }
}