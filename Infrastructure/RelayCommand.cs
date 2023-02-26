using System;
using System.Windows.Input;

namespace Northwind.Infrastructure
{
    // Класс, который хранит в себе набор действий, и который
    // посредством привязки данных будет назначен кнопке на окне
    internal sealed class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object>? predicate;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<object?> execute, 
            Predicate<object?>? predicate = null) 
        {
            this.execute = execute;
            this.predicate = predicate;
        }

        public bool CanExecute(object? parameter)
        {
            return predicate?.Invoke(parameter) == true 
                || predicate == null;
        }

        public void Execute(object? parameter)
        {
           execute?.Invoke(parameter);
        }
    }
}
