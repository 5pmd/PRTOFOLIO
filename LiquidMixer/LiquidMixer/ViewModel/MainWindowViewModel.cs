//using LiquidMixer.Model.Mixer;
//using LiquidMixer.Services;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Channels;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media.Animation;

//namespace LiquidMixer.ViewModel
//{
//    class MainWindowViewModel : INotifyPropertyChanged
//    {

//        private string? _inputLiquidA;
//        private string? _inputLiquidB;
//        private string? _inputLiquidC;
//        private string? _inputLiquidError;
//        private string? _time;
//        private string? _inputTimeError;
//        private bool _canStart = false;
  
//        private readonly LiquidMixerApplication _liquidMixer;


//        public MainWindowViewModel()
//        {
//            MixingModes = Enum.GetValues(typeof(BasicSpeedMode)).Cast<BasicSpeedMode>();
//            StartCommand = new BaseCommand(_ => ExecuteStartCommand(), _ => _canStart);  

//            _liquidMixer = new LiquidMixerApplication( new BasicSpeedHandler(),new Mixer() );
            
//        }
       
       
//        //Liquid Input

//        [LiquidInputValidator("Liquid A")]
//        public string? InputLiquidA
//        {
//            get { return _inputLiquidA; }
//            set
//            {
//                ValidationHelper.ValidateProperty(this, value, nameof(InputLiquidA), _inputLiquidErrors);
//                InputLiquidError = _inputLiquidErrors.Any() ? _inputLiquidErrors.Last().Value.Last() : null;
//                _inputLiquidA = value;
//                ValidateObject();
//            }
//        }

       

//        [LiquidInputValidator("Liquid B")]
//        public string? InputLiquidB
//        {
//            get { return _inputLiquidB; }
//            set
//            {
//                ValidationHelper.ValidateProperty(this, value, nameof(InputLiquidB), _inputLiquidErrors);
//                InputLiquidError = _inputLiquidErrors.Any() ? _inputLiquidErrors.Last().Value.Last() : null;
//                _inputLiquidB = value;
//                ValidateObject();
//            }
//        }
   

//        [LiquidInputValidator("Liquid C")]
//        public string? InputLiquidC
//        {
//            get { return _inputLiquidC; }
//            set
//            {
//                ValidationHelper.ValidateProperty(this, value, nameof(InputLiquidC), _inputLiquidErrors);
//                InputLiquidError = _inputLiquidErrors.Any() ? _inputLiquidErrors.Last().Value.Last() : null;
//                _inputLiquidC = value;
//                ValidateObject();
//            }
//        }


//        // Time Input

//        [TimeInputValidator]
//        public string? Time
//        {
//            get { return _time; }
//            set
//            {
//                ValidationHelper.ValidateProperty(this, value, nameof(Time), _inputTimeErrors);
//                InputTimeError = _inputTimeErrors.Any() ? _inputTimeErrors.Last().Value.Last() : null;
//                _time = value;
//                ValidateObject();

//            }
//        }

//        // ErrorData
//        public string? InputLiquidError
//        {
//            get { return _inputLiquidError; }
//            private set
//            {
//                _inputLiquidError = value;
//                OnPropertyChanged(nameof(InputLiquidError));

//            }
//        }
//        public string? InputTimeError
//        {
//            get { return _inputTimeError; }
//            private set
//            {
//                _inputTimeError = value;
//                OnPropertyChanged(nameof(InputTimeError));


//            }
//        }

//        //Command
//        public BaseCommand StartCommand { get; }
//        private void ExecuteStartCommand()
//        {
           
//            _liquidMixer.Mix();
//        }
      

//        public IEnumerable<BasicSpeedMode> MixingModes { get; }
//        private BasicSpeedMode _selectedMode;
//        public BasicSpeedMode SelectedMode
//        {
//            get { return _selectedMode; }
//            set
//            {
//                _selectedMode = value;
//            }
//        }


//        // Notifier

//        public event PropertyChangedEventHandler? PropertyChanged;

//        private void OnPropertyChanged(string? propertyName)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }


//        private Dictionary<string, List<string?>> _inputLiquidErrors = new();
//        private Dictionary<string, List<string?>> _inputTimeErrors = new();



//        //Object validation
//        private void ValidateObject()
//        {
//            var validationContext = new ValidationContext(this);
//            var result = new List<System.ComponentModel.DataAnnotations.ValidationResult>();

//            if (Validator.TryValidateObject(this, validationContext, result, true))
//            {
//                _canStart = true;
//            }

//            else
//            {
//                _canStart = false;
//            }

//            StartCommand.RaiseCanExecuteChanged();

//        }



//    }



//}
