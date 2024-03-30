using System;
using System.Windows.Input;

namespace OfficeManagementSystem.ViewModels
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            CommandManager.RequerySuggested += OnRequerySuggested;
        }

        ~RelayCommand()
        {
            CommandManager.RequerySuggested -= OnRequerySuggested;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        private void OnRequerySuggested(object sender, EventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
