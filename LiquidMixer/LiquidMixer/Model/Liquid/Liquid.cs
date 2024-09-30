using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model
{
    public class Liquid : ILiquid
    {

        private int _volume;
        private string _name;

        public int Volume
        {
            get => _volume;
            set
            {
                if (value < 0) throw new ArgumentException($"Volume must bigger than 0");
                _volume = value;
            }
        }
        public string Name => _name;

        public Liquid(string name, int volume = 0)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("name can't be Null or WhiteSpace");
            _name = name;
            Volume = volume;
        }

        


    }
}
