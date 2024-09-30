using LiquidMixerApp.Model.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LiquidMixerApp.Services.Command
{
    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<object?, bool> _canExecuted;      
        private readonly Func<object?, Task> _exeCute;
        private bool _isExecuting = false;

        public AsyncRelayCommand(Func<object?, Task> execute, Func<object?, bool> canExecuted)
        {
            _exeCute = execute;
            _canExecuted = canExecuted;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return !_isExecuting && _canExecuted(parameter);
        }

        public async void Execute(object? parameter)
        {
            _isExecuting = true;
            try
            {
                await _exeCute(parameter);
            }
            catch (Exception ex)
            {
                LoggerService.Instance.Log(ex.Message);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }

        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

    }
}
