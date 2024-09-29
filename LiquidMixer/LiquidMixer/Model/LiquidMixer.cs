
using LiquidMixerGUI.Model.Inventory;
using LiquidMixerGUI.Model.Mixer;
using LiquidMixerGUI.Model.SpeedGenerator;
using LiquidMixerGUI.Model.TimeHandler;
using LiquidMixerGUI.Services.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace LiquidMixerGUI.Model;

public class LiquidMixer
{

    private readonly IMixer _mixer;
    private readonly ILiquidInventory _inventory;
    private readonly ITimerHandler _timer;
    private ISpeedGenerator? _speedGenerator;


    public LiquidMixer(IMixer mixer, ILiquidInventory liquidInventory, ITimerHandler timer)
    {
        _mixer = mixer;
        _inventory = liquidInventory;
        _timer = timer;

    }



    private async Task SetTimerHandler(int duration, CancellationToken cancellation)
    {

        try
        {
      
            if (_speedGenerator is not null)
            {
                _timer.OnStopped += _speedGenerator.Stop;
            }
            _timer.OnStopped += _mixer.Stop;
            await _timer.Start(duration, cancellation);
        }
        catch (OperationCanceledException ex)
        {

            throw new OperationCanceledException("Timer Cancelled", ex);
        }

        finally
        {
            _timer.OnStopped -= _mixer.Stop;
           
            if (_speedGenerator is not null)
            {
                _timer.OnStopped -= _speedGenerator.Stop;
            }
        }

    }

    public async Task StartAsync(IEnumerable<Liquid> liquids,  int duration, ISpeedGenerator speedGenerator, CancellationToken cancellation)
    {
        try
        {

            _speedGenerator = speedGenerator;
            LoggerService.Instance.Log($"Liquid Mixer Started");

            await TakeLiquidsFromInventoryAsync(liquids, cancellation);
            var setMixerSpeedTask = SetMixerSpeed(speedGenerator, cancellation);
            StartMixer();
            var setTimerHandlerTask = SetTimerHandler(duration, cancellation);

            await Task.WhenAll(setMixerSpeedTask, setTimerHandlerTask);
            LoggerService.Instance.Log($"Liquid Mixer ");
        }

        finally
        {
            LoggerService.Instance.Log($"Liquid Mixer Finished");

        }

    }


    private async Task TakeLiquidsFromInventoryAsync(IEnumerable<Liquid> liquids, CancellationToken cancellation)
    {
        try
        {
            await _inventory.TakeAsync(liquids, cancellation);
        }
        catch (OperationCanceledException ex)
        {

            throw new OperationCanceledException("Take Liquids from Inventory Cancelled", ex);
        }


    }

    private async Task SetMixerSpeed( ISpeedGenerator speedGenerator, CancellationToken cancellation)
    {


        try
        {
            speedGenerator.OnSpeedGenerated += _mixer.SetSpeed;
            await speedGenerator.GenerateSpeedAsync(cancellation);
        }
        catch (OperationCanceledException ex)
        {

            throw new OperationCanceledException("Speed Generating Cancelled", ex);
        }
        finally
        {
            speedGenerator.OnSpeedGenerated -= _mixer.SetSpeed;
        }
    }

    private void StartMixer()
    {
        _mixer.Start();
    }

    public async Task AddLiquidsToInventory(params Liquid[] liquids)
    {
        await _inventory.AddAsync(liquids);
    }

}
