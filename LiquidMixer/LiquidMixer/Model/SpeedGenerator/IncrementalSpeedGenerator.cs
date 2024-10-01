using LiquidMixerApp.Model.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.SpeedGenerator
{
    public class IncrementalSpeedGenerator : ISpeedGenerator
    {
        private int _step;
        private int _initialSpeed;
        private int _endSpeed;
        private readonly int _delay = 100;
        private CancellationTokenSource? _cancellationTokenSource;

        public IncrementalSpeedGenerator(int initialSpeed, int endSpeed, int step)
        {
            StartSpeed = initialSpeed;
            EndSpeed = endSpeed;
            Step = step;
        }

        private int StartSpeed
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException($"Initial speed must be positive int");
                _initialSpeed = value;
            }
        }

        private int EndSpeed
        {
            set
            {
                if (value < _initialSpeed) throw new ArgumentOutOfRangeException($"End of speed must be bigger than initial speed");
                _endSpeed = value;
            }
        }

        private int Step
        {
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException($"Step must be greater than 0");
                _step = value;
            }
        }

        public event Action<int>? OnSpeedGenerated;

        public async Task GenerateSpeedAsync(CancellationToken cancellation)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);


            for (var currentSpeed = _initialSpeed; currentSpeed <= _endSpeed; currentSpeed += _step)
            {
                _cancellationTokenSource.Token.ThrowIfCancellationRequested();

                await Task.Delay(_delay, _cancellationTokenSource.Token);
                OnSpeedGenerated?.Invoke(currentSpeed);
            }


        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
