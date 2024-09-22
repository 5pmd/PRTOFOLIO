using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.SpeedStrategy
{
    public class IncrementalSpeedGenerator : SpeedGeneratorBase
    {

        private int _duration;
        private int _startSpeed;
        private int _endSpeed;
        private int _step;

        private int StartSpeed
        {
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException($"{nameof(StartSpeed)} must be positive int");
                _startSpeed = value;
            }
        }

        private int EndSpeed
        {

            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException($"{nameof(EndSpeed)} must be positive int");
                _endSpeed = value;
            }
        }

        private int Duration
        {

            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException($"{nameof(Duration)} must be positive int");
                _duration = value;
            }
        }

        public IncrementalSpeedGenerator(int startSpeed, int endSpeed, int duration)
        {

            StartSpeed = startSpeed;
            EndSpeed = endSpeed;
            Duration = duration;
            _step = _step = (int)Math.Ceiling((double)(_endSpeed - _startSpeed) / _duration);
        }


        public override async Task GenerateSpeedAsync()
        {

            for (var currentSpeed = _startSpeed; currentSpeed <= _endSpeed; currentSpeed +=_step)
            {
                await Task.Delay(_step);
                OnSpeedGenerated(this, currentSpeed);
            }
        }

    }
}
