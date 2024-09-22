using LiquidMixerApp;

public class ConsoleUserInteractor : IUserInteractor
{
    public void Write(string message)
    {
       Console.WriteLine(message);
    }
}