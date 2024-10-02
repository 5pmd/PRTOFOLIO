
namespace LiquidMixerApp.Model.Logger
{
    public interface ILoggerService
    {
        event Action<string>? OnLogGenerated;
        void Log(string message);
    }
}