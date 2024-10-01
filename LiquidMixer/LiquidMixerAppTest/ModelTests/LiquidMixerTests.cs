using LiquidMixerApp.Model.Inventory;
using LiquidMixerApp.Model.Mixer;
using LiquidMixerApp.Model.SpeedGenerator;
using LiquidMixerApp.Model.TimeHandler;
using LiquidMixerApp.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LiquidMixerAppTest.ModelTests
{
    [TestFixture]
    internal class LiquidMixerTests
    {
        private Mock<IMixer> _mixerMock;
        private Mock<ILiquidInventory> _inventoryMock;
        private Mock<ITimerHandler> _timerHandlerMock;
        private Mock<ISpeedGenerator> _speedGeneratorMock;
        private LiquidMixer _liquidMixer;

        [SetUp]
        public void SetUp()
        {
            
            _mixerMock = new Mock<IMixer>();
            _inventoryMock = new Mock<ILiquidInventory>();
            _timerHandlerMock = new Mock<ITimerHandler>();
            _speedGeneratorMock = new Mock<ISpeedGenerator>();

           
            _liquidMixer = new LiquidMixer(_mixerMock.Object, _inventoryMock.Object, _timerHandlerMock.Object);
        }

        [Test]
        public async Task StartAsync_ShouldStartMixerAndSetSpeed()
        {
           
            var liquids = new List<Liquid> { new Liquid("Water"), new Liquid("Oil") };
            var duration = 1000;
            var cancellationToken = new CancellationToken();

            _inventoryMock.Setup(inv => inv.TakeAsync(liquids, cancellationToken)).Returns(Task.CompletedTask);
            _timerHandlerMock.Setup(timer => timer.Start(duration, cancellationToken)).Returns(Task.CompletedTask);
            _speedGeneratorMock.Setup(sg => sg.GenerateSpeedAsync(cancellationToken)).Returns(Task.CompletedTask);

          
            await _liquidMixer.StartAsync(liquids, duration, _speedGeneratorMock.Object, cancellationToken);

           
            _mixerMock.Verify(mixer => mixer.Start(), Times.Once);
            _speedGeneratorMock.Verify(sg => sg.GenerateSpeedAsync(cancellationToken), Times.Once);
            _timerHandlerMock.Verify(timer => timer.Start(duration, cancellationToken), Times.Once);
            _inventoryMock.Verify(inv => inv.TakeAsync(liquids, cancellationToken), Times.Once);
        }

        [Test]
        public async Task StartAsync_ShouldStopMixerAndSpeedGenerator_WhenCanceled()
        {
            
            var liquids = new List<Liquid> { new Liquid("Water"), new Liquid("Oil") };
            var duration = 1000;
            var cancellationTokenSource = new CancellationTokenSource();
           
            _inventoryMock.Setup(inv => inv.TakeAsync(liquids, cancellationTokenSource.Token)).Throws(new OperationCanceledException());
            _timerHandlerMock.Setup(timer => timer.Start(duration, cancellationTokenSource.Token)).Throws(new OperationCanceledException());
            _speedGeneratorMock.Setup(sg => sg.GenerateSpeedAsync(cancellationTokenSource.Token)).Throws(new OperationCanceledException());

          
            cancellationTokenSource.Cancel();

            Assert.ThrowsAsync<OperationCanceledException>(() =>
                _liquidMixer.StartAsync(liquids, duration, _speedGeneratorMock.Object, cancellationTokenSource.Token)
            );
 
        }

        [Test]
        public async Task AddLiquidsToInventory_ShouldAddLiquidsToInventory()
        {
           
            var liquids = new Liquid[] { new Liquid("Water"), new Liquid("Oil") };

            _inventoryMock.Setup(inv => inv.AddAsync(liquids)).Returns(Task.CompletedTask);

            
            await _liquidMixer.AddLiquidsToInventory(liquids);

       
            _inventoryMock.Verify(inv => inv.AddAsync(liquids), Times.Once);
        }

        [Test]
        public async Task TakeLiquidsFromInventory_ShouldTakeLiquidsFromInventory()
        {
           
            var liquids = new List<Liquid> { new Liquid("Water"), new Liquid("Oil") };
            var cancellationToken = new CancellationToken();

            _inventoryMock.Setup(inv => inv.TakeAsync(liquids, cancellationToken)).Returns(Task.CompletedTask);

        
            await _liquidMixer.StartAsync(liquids, 1000, _speedGeneratorMock.Object, cancellationToken);

            
            _inventoryMock.Verify(inv => inv.TakeAsync(liquids, cancellationToken), Times.Once);
        }
    }
}
