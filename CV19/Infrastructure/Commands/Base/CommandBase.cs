using System;
using System.Windows.Input;

namespace CV19.Infrastructure.Commands.Base
{
    internal abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value; // Событие RequirySuggested вызывается, если состояние каждой команды нужно обновить. При смене значения свойств в представлении.

            remove => CommandManager.RequerySuggested -= value;
        }
        public abstract bool CanExecute(object command);
        public abstract void Execute(object command);
    }
}
