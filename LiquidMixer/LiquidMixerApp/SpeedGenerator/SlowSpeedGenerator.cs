using LiquidMixerApp.SpeedStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.SpeedGenerator
{
    public class SlowSpeedGenerator : SpeedGeneratorBase
    {
        private readonly int _speed = 50;
        public override Task GenerateSpeedAsync()
        {
            return Task.FromResult(_speed);
        }
    }
}
