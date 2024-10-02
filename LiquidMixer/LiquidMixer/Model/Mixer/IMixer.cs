namespace LiquidMixerApp.Model.Mixer
{
    public interface IMixer
    {      
        void Start();    
        void Stop();
        int Speed { get; }
        void SetSpeed(int speed);      
    }
}