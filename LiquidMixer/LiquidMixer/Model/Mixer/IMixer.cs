namespace LiquidMixerGUI.Model.Mixer
{
    public interface IMixer
    {
        void Start();
        int Speed { get;  }
        void Stop();
        void SetSpeed(int speed);
    }
}