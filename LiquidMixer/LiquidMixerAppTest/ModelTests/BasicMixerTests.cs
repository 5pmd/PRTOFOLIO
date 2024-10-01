using LiquidMixerApp.Model.Logger;
using LiquidMixerApp.Model.Mixer;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerAppTest.ModelTests
{
    [TestFixture]
    public class BasicMixerTests
    {
        private BasicMixer _mixer;

        [SetUp]
        public void SetUp()
        {
            _mixer = new BasicMixer();
        }

        [Test]
        public void SetSpeed_ShallSetSpeedCorrectly()
        {
            _mixer.SetSpeed(100);

            ClassicAssert.AreEqual(100, _mixer.Speed);
        }

        [Test]
        public void SetSpeed_ShalThrowArgumentException_ForSpeedLessThenZero()
        {
            Assert.Throws<ArgumentException>(() => _mixer.SetSpeed(-10));
        }

        [Test]
        public void Stop_ShallSetSpeedToZero()
        {
            _mixer.Stop();
            var speed = 0;

            ClassicAssert.AreEqual(speed, _mixer.Speed);
        }


    }
}
