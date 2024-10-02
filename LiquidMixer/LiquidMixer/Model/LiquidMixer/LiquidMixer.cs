
using LiquidMixerApp.Model.Inventory;
using LiquidMixerApp.Model.Logger;
using LiquidMixerApp.Model.Mixer;
using LiquidMixerApp.Model.SpeedGenerator;
using LiquidMixerApp.Model.TimeHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace LiquidMixerApp.Model;

public class LiquidMixer : ILiquidMixer
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


    public async Task StartAsync(IEnumerable<ILiquid> liquids, int duration, ISpeedGenerator speedGenerator, CancellationToken cancellation)
    {

        _speedGenerator = speedGenerator;
        LoggerService.Instance.Log($"Liquid Mixer Started");

        try
        {
            await TakeLiquidsFromInventoryAsync(liquids, cancellation);
            var setMixerSpeedTask = SetMixerSpeed(speedGenerator, cancellation);
            StartMixer();
            await SetTimerHandler(duration, cancellation);
            LoggerService.Instance.Log($"Liquid Mixer Successfully finished");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Liquid Mixer Not Successfully finished. {ex.Message}");
        }
    }
    public async Task AddLiquidsToInventory(IEnumerable<ILiquid> liquids)
    {
        await _inventory.AddAsync(liquids);
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
    private async Task TakeLiquidsFromInventoryAsync(IEnumerable<ILiquid> liquids, CancellationToken cancellation)
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
    private async Task SetMixerSpeed(ISpeedGenerator speedGenerator, CancellationToken cancellation)
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

}
