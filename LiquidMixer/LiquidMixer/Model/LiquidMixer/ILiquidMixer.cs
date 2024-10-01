using LiquidMixerApp.Model.SpeedGenerator;

namespace LiquidMixerApp.Model
{
    public interface ILiquidMixer
    {
        Task AddLiquidsToInventory(IEnumerable<ILiquid> liquids);
        Task StartAsync(IEnumerable<ILiquid> liquids, int duration, ISpeedGenerator speedGenerator, CancellationToken cancellation);
    }
}