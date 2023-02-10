using CV19.Infrastructure.Commands.Base;
using System;

namespace CV19.Infrastructure.Commands
{
    internal class LamdaCommand : CommandBase
    {
        private readonly Action<object> _Execute;
        private readonly Func<object, bool> _CanExecute;
        public LamdaCommand(Action<object> Execute, Func<object, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object command) => _CanExecute?.Invoke(command) ?? true;
        public override void Execute(object command) => _Execute(command);
    }
}
