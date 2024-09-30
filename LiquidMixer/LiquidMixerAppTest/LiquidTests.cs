using LiquidMixerApp.Model;
using LiquidMixerApp.Model.Inventory;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerAppTest
{
    [TestFixture]
    public class LiquidTests
    {
        [Test]
       public void LiquidInstance_ShallReturnTheCorrectVolume()
        {
            var liquid = new Liquid("Water", 100);

            var result = liquid.Volume;

           ClassicAssert.AreEqual(100, result);
        }


        [Test]
        public void LiquidInstance_ShallReturnTheCorrectName()
        {
            var liquid = new Liquid("Water", 100);

            var result = liquid.Name;

            ClassicAssert.AreEqual("Water", result);
        }


        [Test]
        public void LiquidCreation_ShallThrowArgumentException_WhenVolumeLessThenZero()
        {
            Assert.Throws<ArgumentException>(() => new Liquid("Water", -100));
        }

        [Test]
        public void LiquidCreation_ShallThrowArgumentException_WhenNameIsWhiteSpace()
        {
            Assert.Throws<ArgumentException>(() => new Liquid(" ", 100));
        }
        

    }
}
