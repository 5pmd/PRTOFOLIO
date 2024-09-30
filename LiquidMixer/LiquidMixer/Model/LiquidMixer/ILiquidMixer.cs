using LiquidMixerApp.Model.SpeedGenerator;

namespace LiquidMixerApp.Model
{
    public interface ILiquidMixer
    {
        Task AddLiquidsToInventory(params Liquid[] liquids);
        Task StartAsync(IEnumerable<Liquid> liquids, int duration, ISpeedGenerator speedGenerator, CancellationToken cancellation);
    }
}