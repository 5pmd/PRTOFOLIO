using LiquidMixerApp.Model.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace LiquidMixerApp.Model.Mixer
{
    public class BasicMixer : IMixer
    {
        private int _speed;

        public int Speed => _speed;

        public void SetSpeed(int speed)
        {
            if (speed < 0) throw new ArgumentException("Speed must positive int");
            _speed = speed;
            LoggerService.Instance.Log($"Set Speed for {speed} rpm");
        }

        public void Start()
        {
            LoggerService.Instance.Log($"Start Mixer with speed {_speed}rpm");
        }

        public void Stop()
        {
            _speed = 0;
            LoggerService.Instance.Log($"Mixer stop and set speed for {_speed}rpm");
        }
    }
}
