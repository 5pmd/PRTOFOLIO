using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger.Model;

namespace Logger.ViewModel
{
    public class LoggerViewModel : INotifyPropertyChanged
    {
        private MyLogger _logger;

        public LoggerViewModel()
        {
            _logger = new MyLogger();
        }

        public ObservableCollection<string> LogMessages => _logger.LogMessages;

        public void LogActivity(string activity)
        {
            _logger.Log(activity);
           // OnPropertyChanged(nameof(LogMessages));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
