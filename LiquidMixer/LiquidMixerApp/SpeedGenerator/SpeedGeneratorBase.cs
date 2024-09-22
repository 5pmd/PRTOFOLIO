using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.SpeedStrategy
{
    public abstract class SpeedGeneratorBase
    {

        public event EventHandler<int>? SpeedGenerated;

        public abstract Task GenerateSpeedAsync();
        protected void OnSpeedGenerated(object? sender, int speed)
        {
            SpeedGenerated?.Invoke(sender, speed);
        }


    }
}
