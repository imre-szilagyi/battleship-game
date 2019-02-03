using System;
using System.Windows.Input;

namespace Battleship.Wpf.GameBoard.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object> _executeCallback;
        private Func<object, bool> _canExecuteCallback;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(Action<object> executeCallback, Func<object, bool> canExecuteCallback = null)
        {
            _executeCallback = executeCallback ?? throw new ArgumentNullException(nameof(executeCallback));
            _canExecuteCallback = canExecuteCallback;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecuteCallback != null)
                return _canExecuteCallback.Invoke(parameter);

            return true;
        }

        public void Execute(object parameter)
        {
            _executeCallback(parameter);
        }
    }
}
