using System;
using System.Windows;
using System.Windows.Input;

namespace Northwind.Infrastructure
{
    internal class SafeRelayCommand : ICommand
    {
        private readonly ICommand command;

        public event EventHandler? CanExecuteChanged;

        public SafeRelayCommand(ICommand command)
        {
            this.command = command;
        }

        public bool CanExecute(object? parameter)
        {
            return command.CanExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            try
            {
                command.Execute(parameter);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
