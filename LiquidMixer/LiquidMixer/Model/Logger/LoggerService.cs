using LiquidMixerApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.Logger
{
    public class LoggerService : ILoggerService
    {
        private static LoggerService? _instance;
        public static LoggerService Instance => _instance ??= new LoggerService();
        private LoggerService() { }


        public event Action<string>? OnLogGenerated;
        public void Log(string message) => OnLogGenerated?.Invoke(message);

    }

}
