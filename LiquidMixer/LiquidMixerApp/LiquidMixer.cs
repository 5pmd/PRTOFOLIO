using LiquidMixerApp.Inventory;
using LiquidMixerApp.Liquids;
using LiquidMixerApp.Mixer;
using LiquidMixerApp.SpeedStrategy;
using LiquidMixerApp.Timer;
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
        private readonly IUserInteractor _userInteractor;
        private readonly TimeHandlerBase _timer;
        private readonly ILogger _logger;
        private readonly int duration;

        public LiquidMixer(IMixer mixer, ILiquidInventory liquidInventory, SpeedGeneratorBase speedGenerator, IUserInteractor userInteractor, ILogger logger, TimeHandlerBase timer)
        {
            _mixer = mixer;
            _inventory = liquidInventory;
            _speedGenerator = speedGenerator;
            _userInteractor = userInteractor;
            _logger = logger;
            _timer = timer;
        }


        public async Task StartAsync( int duration, params Liquid[] liquids)
        {
            if (!await IsLiquidsAvailable(liquids))
            {
                _userInteractor.Write("Liquids not available !");
            }
            try
            {
                _userInteractor.Write("Start has been Press");
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
          
            _userInteractor.Write("Stop the Application");
        }


    }
}
