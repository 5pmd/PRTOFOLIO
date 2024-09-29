using LiquidMixerApp;

namespace LiquidMixerApp
{
    public interface ILogger
    {
        void Write(string message);
    }
}

public class LocalLogger : ILogger
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}