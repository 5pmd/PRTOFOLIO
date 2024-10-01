namespace LiquidMixerApp.Model.SpeedGenerator
{
    public interface ISpeedGeneratorFactory
    {
        ISpeedGenerator GetSpeedGenerator(BasicSpeedsMode mode);
    }
}