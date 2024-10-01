using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LiquidMixerApp.Model.Inventory;
using LiquidMixerApp.Model;
using System.Timers;
using NUnit.Framework.Legacy;

namespace LiquidMixerAppTest.ModelTests
{
    [TestFixture]
    public class LiquidInventoryTests
    {
        private LocalLiquidInventory _inventory;

        [SetUp]
        public void SetUp()
        {
            _inventory = new LocalLiquidInventory();
        }


        [Test]
        public async Task AddAsync_ShouldAddLiquidsToInventory()
        {
            var liquids = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var liquidsToTake = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };

            await _inventory.AddAsync(liquids);

            Assert.DoesNotThrowAsync(() => _inventory.TakeAsync(liquidsToTake, CancellationToken.None));
        }

        [Test]
        public async Task AddAsync_ShouldIncreaseVolume_ForExistingLiquidsName()
        {
            var firstLiquids = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var secondLiquids = new[] { new Liquid("Water", 50), new Liquid("Oil", 50) };
            var liquidsToTake = new[] { new Liquid("Water", 150), new Liquid("Oil", 250) };
            await _inventory.AddAsync(firstLiquids);

            await _inventory.AddAsync(secondLiquids);

            Assert.DoesNotThrowAsync(() => _inventory.TakeAsync(liquidsToTake, CancellationToken.None));
        }

        [Test]
        public async Task AddAsync_ShouldHandleMultiThreading()
        {
            var firstLiquids = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var secondLiquids = new[] { new Liquid("Water", 50), new Liquid("Oil", 50) };
            var liquidsToTake = new[] { new Liquid("Water", 150), new Liquid("Oil", 250) };

            var firstTask = _inventory.AddAsync(firstLiquids);
            var secondTask = _inventory.AddAsync(secondLiquids);

            await Task.WhenAll(firstTask, secondTask);

            Assert.DoesNotThrowAsync(() => _inventory.TakeAsync(liquidsToTake, CancellationToken.None));
        }

        [Test]
        public async Task TakeAsync_ShouldDecreaseExistingVolume_ForAvailableLiquids()
        {
            var liquids = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var firstLiquidsToTake = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var secondLiquidsToTake = new[] { new Liquid("Water", 1), new Liquid("Oil", 1) };
            await _inventory.AddAsync(liquids);


            await _inventory.TakeAsync(firstLiquidsToTake, CancellationToken.None);

            Assert.ThrowsAsync<ArgumentException>(() => _inventory.TakeAsync(secondLiquidsToTake, CancellationToken.None));

        }

        [Test]
        public void TakeAsync_ShouldHandleMultiThread()
        {
            var liquids = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var liquidsToTake = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };

            var addTask = _inventory.AddAsync(liquids);

            Assert.DoesNotThrowAsync(() => _inventory.TakeAsync(liquidsToTake, CancellationToken.None));
        }


        [Test]
        public async Task TakeAsync_ShouldCancelled_WhenCancelRequested()
        {
            var liquids = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var liquidsToTake = new[] { new Liquid("Water", 100), new Liquid("Oil", 200) };
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Cancel();
            await _inventory.AddAsync(liquids);

            Assert.ThrowsAsync<OperationCanceledException>(() => _inventory.TakeAsync(liquidsToTake, cancellationTokenSource.Token));

        }



    }

}
