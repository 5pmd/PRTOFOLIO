using LiquidMixerApp.Model.TimeHandler;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerAppTest
{
    [TestFixture]
    internal class DelayTimeHandlerTests
    {
        private ITimerHandler _timerHandler;
        [SetUp]
        public void SetUp()
        {
            _timerHandler = new DelayTimeHandler();
        }

        [Test]
        public async Task Start_ShouldTriggerOnStopped_WhenDurationCompletes()
        {
            
            bool onStoppedCalled = false;
            _timerHandler.OnStopped += () => onStoppedCalled = true;
          
            await _timerHandler.Start(100, CancellationToken.None);
        
            ClassicAssert.IsTrue(onStoppedCalled);         
        }

        [Test]
        public void Start_ShouldThrowOperationCanceledException_WhenCancellationIsRequested()
        {
           
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel(); 

            
            Assert.ThrowsAsync<OperationCanceledException>(async () =>
                await _timerHandler.Start(1000, cancellationTokenSource.Token));
        }

        [Test]
        public async Task Start_ShouldTriggerOnStopped_WhenCancelled()
        {
          
            bool onStoppedCalled = false;
            _timerHandler.OnStopped += () => onStoppedCalled = true;

            var cancellationTokenSource = new CancellationTokenSource();
            var task = _timerHandler.Start(5000, cancellationTokenSource.Token);
            cancellationTokenSource.Cancel();


            Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
            ClassicAssert.IsTrue(onStoppedCalled);
        }
    }
}
