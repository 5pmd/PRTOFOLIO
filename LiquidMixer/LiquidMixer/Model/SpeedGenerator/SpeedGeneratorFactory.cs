using LiquidMixerApp.Model.SpeedGenerator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.SpeedGenerator
{
    public class SpeedGeneratorFactory : ISpeedGeneratorFactory
    {

        public ISpeedGenerator GetSpeedGenerator(BasicSpeedsMode mode) => mode switch
        {
            BasicSpeedsMode.FAST => new FastSpeedGenerator(),
            BasicSpeedsMode.NORMAL => new NormalSpeedGenerator(),
            BasicSpeedsMode.SLOW => new SlowSpeedGenerator(),
            BasicSpeedsMode.INCREMENTAL => new IncrementalSpeedGenerator(0, 5000, 20),
            _ => throw new ArgumentException("Mode doesn't exist")

        };


    }
}




