namespace LiquidMixerApp.Model
{
    public interface ILiquid
    {
        string Name { get; }
        int Volume { get; set; }

        bool Equals(object? obj);
        int GetHashCode();
    }
}