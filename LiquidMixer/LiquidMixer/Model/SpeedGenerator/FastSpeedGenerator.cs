
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.SpeedGenerator
{
    public class FastSpeedGenerator : ISpeedGenerator
    {
        private CancellationTokenSource? _cancellationTokenSource;
        private readonly int _speed = 500;

        public event Action<int>? OnSpeedGenerated;

        public async Task GenerateSpeedAsync(CancellationToken cancellation)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
            CancellationTokenSource.CreateLinkedTokenSource(cancellation);

            cancellation.ThrowIfCancellationRequested();
            await Task.Delay(500);
            OnSpeedGenerated?.Invoke(_speed);


        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();

        }
    }
}
