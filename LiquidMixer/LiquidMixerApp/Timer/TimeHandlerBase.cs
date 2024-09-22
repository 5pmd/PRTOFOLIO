
namespace LiquidMixerApp.Timer
{
    public abstract class TimeHandlerBase
    {
        public event EventHandler<bool>? Finished;

        public abstract Task Start(int duration);

        public void OnFinished(object? sender, bool isFinished)
        {
            Finished?.Invoke(this, isFinished);
        }
    }
}