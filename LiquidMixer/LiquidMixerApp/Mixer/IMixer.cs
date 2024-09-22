namespace LiquidMixerApp.Mixer
{
    public interface IMixer
    {
        void Start();
        int Speed { get; protected set; }
        void Stop(object? sender, bool isFinished);
        void SetSpeed(object? sender, int speed);
    }
}