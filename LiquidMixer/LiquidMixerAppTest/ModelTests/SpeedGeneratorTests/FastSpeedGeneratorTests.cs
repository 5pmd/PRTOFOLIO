using LiquidMixerApp.Model.SpeedGenerator;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Printing.PrintSupport;
using Windows.Media.SpeechSynthesis;

namespace LiquidMixerAppTest.ModelTests.SpeedGeneratorTests
{
    internal class FastSpeedGeneratorTests
    {

        private ISpeedGenerator _speedGenerator;

        [SetUp]
        public void SetUp()
        {
            _speedGenerator = new FastSpeedGenerator();
        }

        [Test]
        public async Task GenerateSpeedAsync_ShouldGenerate500()
        {
            var expectedSpeed = 500;
            var speed = 0;
            _speedGenerator.OnSpeedGenerated += (generatedSpeed) => speed = generatedSpeed;

            await _speedGenerator.GenerateSpeedAsync(CancellationToken.None);


            ClassicAssert.AreEqual(expectedSpeed, speed);

        }

        [Test]
        public async Task Stop_ShouldCancelSpeedGenerating()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            _speedGenerator.Stop();

            Assert.ThrowsAsync<OperationCanceledException>(() => _speedGenerator.GenerateSpeedAsync(cancellationTokenSource.Token));
        }




    }
}
