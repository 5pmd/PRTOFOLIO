using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger.Model
{
    public class MyLogger
    {
        // Observable collection to store log messages
        public ObservableCollection<string> LogMessages { get; private set; }

        public MyLogger()
        {
            LogMessages = new ObservableCollection<string>();
        }

        // Method to add log messages
        public void Log(string message)
        {
            string logMessage = $"{DateTime.Now}: {message}";
            LogMessages.Add(logMessage);
        }
    }
}
