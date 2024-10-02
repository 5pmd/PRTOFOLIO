
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace LiquidMixerApp.Model.SpeedGenerator
{
    public interface ISpeedGenerator
    {
        event Action<int>? OnSpeedGenerated;
        Task GenerateSpeedAsync(CancellationToken cancellation);
        void Stop();
    }
}
