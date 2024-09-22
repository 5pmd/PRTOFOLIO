using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixer.Model.Liquids
{
    public abstract class Liquid
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
        protected Liquid(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name can't be Null or WhiteSpace");
            _name = name;
        }


    }
}
