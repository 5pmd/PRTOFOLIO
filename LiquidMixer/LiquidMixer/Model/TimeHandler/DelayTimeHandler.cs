using LiquidMixerApp.Model.Logger;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.TimeHandler
{
    public class DelayTimeHandler : ITimerHandler
    {
        public event Action? OnStopped;

        public async Task Start(int duration, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();  
            LoggerService.Instance.Log($"Start Timer for {duration} ms");

            try
            {
                await Task.Delay(duration , cancellation);  
            } 
            finally 
            {
                OnStopped?.Invoke();
            }
        }
    }
}
