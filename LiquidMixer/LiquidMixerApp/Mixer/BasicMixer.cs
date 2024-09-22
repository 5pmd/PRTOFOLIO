using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Mixer
{
    public class BasicMixer : IMixer
    {

        private int _speed;
        public int Speed { get => _speed;  set => _speed = value; }

        public void SetSpeed(object? sender, int speed)
        {
            Speed = speed;
            Console.WriteLine($"Set Speed for {speed}");

        }

        public void Start()
        {
            Console.WriteLine($"Start Mixer with speed {_speed}");
        }

        public void Stop(object? sender, bool isFinished)
        {
            Console.WriteLine("Stop Mixer");
        }
    }
}
