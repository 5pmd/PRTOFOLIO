using LiquidMixerApp.Model.SpeedGenerator;
using NUnit.Framework.Legacy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerAppTest.ModelTests.SpeedGeneratorTests
{
    internal class IncrementalSpeedGeneratorTest
    {
        private ISpeedGenerator _speedGenerator;

        [SetUp]
        public void SetUp()
        {
            _speedGenerator = new FastSpeedGenerator();
        }


        [TestCase(-1, 10, 1)]
        [TestCase(10, 5, 1)]
        [TestCase(10, 20, 0)]
        public void Constructor_ShouldThrowArgumentOutOfRangeException_OnInvalidParameters(int initialSpeed, int endSpeed, int step)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new IncrementalSpeedGenerator(initialSpeed, endSpeed, step));
        }

        [Test]
        public async Task GenerateSpeedAsync_ShouldGenerateSpeedsCorrectly()
        {

            var generator = new IncrementalSpeedGenerator(0, 10, 2);
            var expected = new List<int>() { 0, 2, 4, 6, 8, 10 };
            var generatedSpeeds = new List<int>();
            generator.OnSpeedGenerated += (speed) => generatedSpeeds.Add(speed);

            await generator.GenerateSpeedAsync(CancellationToken.None);

            CollectionAssert.AreEqual(expected, generatedSpeeds);
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
