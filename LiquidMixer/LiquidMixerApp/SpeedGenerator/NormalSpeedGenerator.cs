using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.SpeedStrategy
{
    public class NormalSpeedGenerator : SpeedGeneratorBase
    {

        private readonly int _speed = 100;
        
        public override Task GenerateSpeedAsync()
        {
           OnSpeedGenerated(this, _speed);
           return Task.FromResult(_speed);
        }

    }

}
