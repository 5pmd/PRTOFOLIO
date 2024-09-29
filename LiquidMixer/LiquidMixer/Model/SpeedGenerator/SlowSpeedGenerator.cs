
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerGUI.Model.SpeedGenerator
{
    public class SlowSpeedGenerator : ISpeedGenerator
    {
        private readonly int _speed = 50;
        private  CancellationTokenSource? _cancellationTokenSource;

        public event Action<int>? OnSpeedGenerated;

        public async Task GenerateSpeedAsync(CancellationToken cancellation)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
            _cancellationTokenSource.Token.ThrowIfCancellationRequested();
            await Task.FromResult(_speed);
            OnSpeedGenerated?.Invoke(_speed);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            
        }
    }
}
