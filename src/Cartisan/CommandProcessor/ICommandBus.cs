namespace Cartisan.CommandProcessor {
    public interface ICommandBus {
        void Submit<TCommand>(TCommand command) where TCommand: ICommand;
    }
}