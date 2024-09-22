using LiquidMixerApp.SpeedStrategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.SpeedGenerator
{
    public class FastSpeedGenerator : SpeedGeneratorBase
    {
        private readonly int _speed;
             
        public override Task GenerateSpeedAsync()
        {
            return Task.FromResult(_speed);
        }

       
    }
}
