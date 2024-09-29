using LiquidMixerApp.Inventory;
using LiquidMixerApp.Liquids;
using LiquidMixerApp.Mixer;
using LiquidMixerApp.SpeedStrategy;
using LiquidMixerApp.Timer;
using LiquidMixerApp.UserInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp
{
    public class LiquidMixer
    {

        private readonly IMixer _mixer;
        private readonly ILiquidInventory _inventory;
        private readonly SpeedGeneratorBase _speedGenerator;
        private readonly TimeHandlerBase _timer;
        private readonly ILogger _logger;
        private readonly int duration;

        public LiquidMixer(IMixer mixer, ILiquidInventory liquidInventory, SpeedGeneratorBase speedGenerator, TimeHandlerBase timer, ILogger logger)
        {
            _mixer = mixer;
            _inventory = liquidInventory;
            _speedGenerator = speedGenerator;         
            _logger = logger;
            _timer = timer;
        }


        public async Task StartAsync( int duration, params Liquid[] liquids)
        {
            if (!await IsLiquidsAvailable(liquids))
            {
                _logger.Write("Liquids not available !");
            }
            try
            {
                _logger.Write("Start has been Press");
                await _inventory.TakeAsync(liquids);
                _speedGenerator.SpeedGenerated += _mixer.SetSpeed;
                var speedGenerateTask =_speedGenerator.GenerateSpeedAsync();
                var startTimer = _timer.Start(duration);
                _mixer.Start();
                _timer.Finished += _mixer.Stop;
                Task.WaitAll(startTimer);
            }
            catch (Exception)
            {

                throw;
            }


        }

        private async Task<bool> IsLiquidsAvailable(params Liquid[] liquids)
        {
            return await _inventory.IsAvailableAsync(liquids);
        }

        public void Stop()
        {
          
            _logger.Write("Stop the Application");
        }


    }
}
