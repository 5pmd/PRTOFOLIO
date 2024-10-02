using LiquidMixerApp.Model;
using LiquidMixerApp.Model.Inventory;
using LiquidMixerApp.Model.Logger;
using LiquidMixerApp.Model.Mixer;
using LiquidMixerApp.Model.SpeedGenerator;
using LiquidMixerApp.Model.TimeHandler;
using LiquidMixerApp.Services.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace LiquidMixerApp.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ILiquid _liquidAToMix;
        private readonly ILiquid _liquidBToMix;
        private readonly ILiquid _liquidCToMix;
        private IEnumerable<ILiquid> _liquidsToMix;


        private readonly ILiquid _liquidAToAdd;
        private readonly ILiquid _liquidBToAdd;
        private readonly ILiquid _liquidCToAdd;
        private IEnumerable<ILiquid> _liquidsToAdd;

        private readonly ISpeedGeneratorFactory _speedGeneratorFactory;
        private readonly ILiquidMixer _liquidMixer;
        private CancellationTokenSource _cancellationTokenSource;

        private string? _duration = null;
        private readonly Dictionary<string, string?> _inputLiquidToMixErrors;
        private readonly Dictionary<string, string?> _inputLiquidToAddErrors;
        private readonly Dictionary<string, string?> _inputDurationErrors;
        private bool _canStop = false;

        public ObservableCollection<string> LogEntries { get; }
        public AsyncRelayCommand StartCommand { get; }
        public RelayCommand StopCommand { get; }
        public AsyncRelayCommand AddCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        public MainWindowViewModel((ILiquid, ILiquid, ILiquid) liquidsToMix, (ILiquid, ILiquid, ILiquid) liquidsToAdd, ISpeedGeneratorFactory speedGeneratorFactory, ILiquidMixer liquidMixer)
        {
            _liquidAToMix = liquidsToMix.Item1;          
            _liquidBToMix = liquidsToMix.Item2;
            _liquidCToMix = liquidsToMix.Item3;
            _liquidsToMix = new[] { _liquidAToMix, _liquidBToMix, _liquidCToMix };

            _liquidAToAdd = liquidsToAdd.Item1;
            _liquidBToAdd = liquidsToAdd.Item2;
            _liquidCToAdd = liquidsToAdd.Item3;
            _liquidsToAdd = new[] { _liquidAToAdd, _liquidBToAdd, _liquidCToAdd };

            _inputLiquidToMixErrors = new Dictionary<string, string?>();
            _inputDurationErrors = new Dictionary<string, string?>();
            _inputLiquidToAddErrors = new Dictionary<string, string?>();

            _speedGeneratorFactory = speedGeneratorFactory;
            _liquidMixer = liquidMixer;
            _cancellationTokenSource = new CancellationTokenSource();

            LogEntries = new ObservableCollection<string>();
            LoggerService.Instance.OnLogGenerated += AddLog;

            StartCommand = new AsyncRelayCommand(exe => StartAsync(), canExe => CanStart());
            StopCommand = new RelayCommand(exe => Stop(), canExe => CanStop());
            AddCommand = new AsyncRelayCommand(exe => AddLiquidsToInventory(), canExe => CanAddLiquidsToInventory());
        }

        private bool CanAddLiquidsToInventory()
        {
            return !_inputLiquidToAddErrors.Any();
        }
        private async Task AddLiquidsToInventory()
        {
            AddCommand.RaiseCanExecuteChanged();
            await _liquidMixer.AddLiquidsToInventory(_liquidsToAdd);
        }

        public Array SpeedsMode => Enum.GetValues(typeof(BasicSpeedsMode));
        private BasicSpeedsMode _selectedSpeedMode;
        public BasicSpeedsMode SelectedSpeedMode
        {
            get => _selectedSpeedMode;
            set
            {
                if (_selectedSpeedMode != value)
                {
                    _selectedSpeedMode = value;
                    OnPropertyChanged(nameof(SelectedSpeedMode));
                }
            }
        }


        //MIX
        public ILiquid LiquidAToMix => _liquidAToMix;  
        public ILiquid LiquidBToMix => _liquidBToMix;
        public ILiquid LiquidCToMix => _liquidCToMix;

        private string? _liquidAVolumeToMix = null;

        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Required(ErrorMessage = "Liquid A volume is required!")]
        public string? LiquidAVolumeToMix
        {
            get => _liquidAVolumeToMix;

            set
            {
                _liquidAVolumeToMix = value;
                SetLiquidVolumeToMix(value, _liquidAToMix, nameof(LiquidAVolumeToMix));
                ValidateTotalLiquidsVolumeToMix();
            }
        }

        private string? _liquidBVolumeToMix = null;

        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Required(ErrorMessage = "Liquid B volume is required!")]
        public string? LiquidBVolumeToMix
        {
            get => _liquidBVolumeToMix;
            set
            {
                _liquidBVolumeToMix = value;
                SetLiquidVolumeToMix(value, _liquidBToMix, nameof(LiquidBVolumeToMix));
                ValidateTotalLiquidsVolumeToMix();
            }
        }

        private string? _liquidCVolumeToMix = null;

        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Required(ErrorMessage = "Liquid C volume is required!")]
        public string? LiquidCVolumeToMix
        {
            get => _liquidCVolumeToMix;
            set
            {
                _liquidCVolumeToMix = value;
                SetLiquidVolumeToMix(value, _liquidCToMix, nameof(LiquidCVolumeToMix));
                ValidateTotalLiquidsVolumeToMix();
            }
        }


        [Required(ErrorMessage = "Duration is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Range(1, 10000, ErrorMessage = "Duration must be between 1 and 10000!")]
        public string? Duration
        {
            get => _duration;
            set
            {
                _duration = value;
                ValidateProperty(value, nameof(Duration), _inputDurationErrors);
                StartCommand.RaiseCanExecuteChanged();
                OnPropertyChanged(nameof(Duration));
                OnPropertyChanged(nameof(LatestDurationError));
                if (int.TryParse(Duration, out var duration))
                {

                }
            }
        }

        //INVENTORY
        public ILiquid LiquidAToAdd => _liquidAToAdd;
        public ILiquid LiquidBToAdd => _liquidBToAdd;
        public ILiquid LiquidCToAdd => _liquidCToAdd;

        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Required(ErrorMessage = "Liquid A volume is required!")]
        [Range(0, 100, ErrorMessage = "Liquid A Volume must be between 0 and 100!")]
        public string? LiquidAVolumeToAdd
        {
            get => _liquidAToAdd.Volume.ToString();
            set => SetLiquidVolumeToAdd(value, _liquidAToAdd, nameof(LiquidAVolumeToAdd));

        }

        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Required(ErrorMessage = "Liquid B volume is required!")]
        [Range(0, 100, ErrorMessage = "Liquid B Volume must be between 0 and 100!")]
        public string? LiquidBVolumeToAdd
        {
            get => _liquidBToAdd.Volume.ToString();
            set => SetLiquidVolumeToAdd(value, _liquidBToAdd, nameof(LiquidBVolumeToAdd));
        }

        [RegularExpression(@"^\d+$", ErrorMessage = "Only digits are allowed.")]
        [Required(ErrorMessage = "Liquid C volume is required!")]
        [Range(0, 100, ErrorMessage = "Liquid C Volume must be between 0 and 100!")]
        public string? LiquidCVolumeToAdd
        {
            get => _liquidCToAdd.Volume.ToString();
            set => SetLiquidVolumeToAdd(value, _liquidCToAdd, nameof(LiquidCVolumeToAdd));
        }


        [Range(0, 100, ErrorMessage = "Total Volume to mix must <= 100")]
        public int TotalLiquidsVolumeToMix => _liquidAToMix.Volume + _liquidBToMix.Volume + _liquidCToMix.Volume;

        public string? LatestLiquidsToMixError => _inputLiquidToMixErrors.LastOrDefault().Value;
        public string? LatestLiquidsToAddError => _inputLiquidToAddErrors.LastOrDefault().Value;
        public string? LatestDurationError => _inputDurationErrors.LastOrDefault().Value;

        private int GetDuration()
        {
            return int.TryParse(_duration, out int duration) ? duration : 0;
        }

        private async Task StartAsync()
        {

            StartCommand.RaiseCanExecuteChanged();
            _canStop = true;
            var liquidsToMix = new[] { _liquidAToMix, _liquidBToMix, _liquidCToMix };
            var duration = GetDuration();

            StopCommand.RaiseCanExecuteChanged();

            var speedGenerator = _speedGeneratorFactory.GetSpeedGenerator(SelectedSpeedMode);


            try
            {
                LogEntries.Clear();
                await _liquidMixer.StartAsync(liquidsToMix, duration, speedGenerator, _cancellationTokenSource.Token);
                LoggerService.Instance.Log("Mixing Completed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                LoggerService.Instance.Log(ex.Message);
            }
            finally
            {
                _cancellationTokenSource.Dispose();
                _cancellationTokenSource = new CancellationTokenSource();
                _canStop = false;
                StopCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanStart()
        {
            return LatestDurationError is null && LatestLiquidsToMixError is null;
        }

        private void Stop()
        {
            MessageBox.Show("Stop");
            _cancellationTokenSource.Cancel();
        }
        private bool CanStop() => _canStop;

        private void SetLiquidVolumeToMix(string? value, ILiquid liquid, string propertyName)
        {

            ValidateProperty(value, propertyName, _inputLiquidToMixErrors);

            if (int.TryParse(value, out var volume))
            {
                liquid.Volume = volume;
            }

            StartCommand.RaiseCanExecuteChanged();
            OnPropertyChanged(propertyName);
            OnPropertyChanged(nameof(LatestLiquidsToMixError));
        }
        private void ValidateTotalLiquidsVolumeToMix()
        {
            ValidateProperty(TotalLiquidsVolumeToMix, nameof(TotalLiquidsVolumeToMix), _inputLiquidToMixErrors);
            OnPropertyChanged(nameof(TotalLiquidsVolumeToMix));
            StartCommand.RaiseCanExecuteChanged();
            OnPropertyChanged(nameof(LatestLiquidsToMixError));
        }

        private void SetLiquidVolumeToAdd(string? value, ILiquid liquid, string propertyName)
        {

            ValidateProperty(value, propertyName, _inputLiquidToAddErrors);

            if (int.TryParse(value, out var volume))
            {
                liquid.Volume = volume;
            }

            OnPropertyChanged(propertyName);
            OnPropertyChanged(nameof(LatestLiquidsToAddError));
            AddCommand.RaiseCanExecuteChanged();

        }

        private void ValidateProperty(object? propertyValue, string propertyName, Dictionary<string, string?> result)
        {
            result.Remove(propertyName);
            var validationContext = new ValidationContext(this) { MemberName = propertyName };
            var validationResult = new List<ValidationResult>();
            Validator.TryValidateProperty(propertyValue, validationContext, validationResult);

            if (validationResult.Any())
            {
                result[propertyName] = validationResult[0].ErrorMessage;
            }

            OnPropertyChanged(propertyName);
        }

        private void AddLog(string message) => LogEntries.Add(message);

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

       
    }
}
