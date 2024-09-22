using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LiquidMixer.Services
{
    internal class BaseCommand : ICommand
    {

        private readonly Func<object?, bool> _canExecuted;
        private readonly Action<object?> _exeCute;


        public BaseCommand(Action<object?> exeCute, Func<object?, bool> canExecuted)
        {
            _exeCute = exeCute;
            _canExecuted = canExecuted;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canExecuted(parameter);
        }

        public void Execute(object? parameter)
        {
           _exeCute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }


    }
}
