using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Timer
{
    public class TimeHandler : TimeHandlerBase
    {
  
        public async override Task Start(int duration)
        {
            Console.WriteLine($"Start Timer and the duration is {duration}");
            OnFinished(this, false);
            await Task.Delay(duration);
            OnFinished(this, true);
            Console.WriteLine("Time Ended");
        }
      
    }
}
