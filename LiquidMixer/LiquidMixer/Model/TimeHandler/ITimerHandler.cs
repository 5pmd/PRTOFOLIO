namespace LiquidMixerGUI.Model.TimeHandler
{
    public interface  ITimerHandler
    {
         event Action? OnStopped;

         Task Start(int duration, CancellationToken cancellation);

      
    }
}