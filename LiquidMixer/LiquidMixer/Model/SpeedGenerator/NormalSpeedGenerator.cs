using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerGUI.Model.SpeedGenerator
{
    public class NormalSpeedGenerator : ISpeedGenerator
    {

        private readonly int _speed = 200;
        private CancellationTokenSource? _cancellationTokenSource;
        public event Action<int>? OnSpeedGenerated;

        public async Task GenerateSpeedAsync(CancellationToken cancellation)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellation);

            cancellation.ThrowIfCancellationRequested();
            await Task.FromResult(_speed);
            OnSpeedGenerated?.Invoke(_speed); 
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }
    }

}
