using System;
using Cartisan.CommandProcessor;

namespace Cartisan.Identity.Service.Commands {
    public class DeleteUser: ICommand {
        public Guid UserId { get; set; } 
    }
}