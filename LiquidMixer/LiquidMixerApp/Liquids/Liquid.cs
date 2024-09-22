using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Liquids
{
    public class Liquid
    {

        private int _volume;
        private string _name;

        public int Volume
        {
            get { return _volume; }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException($"Volume must bigger than 0");
                _volume = value;
            }
        }
        public string Name
        {
            get { return _name; }

        }
        public Liquid(string name, int volume)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name can't be Null or WhiteSpace");
            _name = name;
            Volume = volume;
        }

        public override bool Equals(object? obj)
        {
            var objAsLiquid = obj as Liquid;
            if (objAsLiquid == null) return false;

            return objAsLiquid.Name == _name;
        }

        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
}
