using LiquidMixerApp.Model.Logger;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.Inventory
{
    public class LocalLiquidInventory : ILiquidInventory
    {
        private readonly Dictionary<string, ILiquid> _liquids;
        private readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1, 1);

        public LocalLiquidInventory()
        {
            _liquids = new Dictionary<string, ILiquid>();
        }

        public async Task AddAsync(IEnumerable<ILiquid> liquids)
        {
            await _semaphoreSlim.WaitAsync(); 
            try
            {
                foreach (var liquidToAdd in liquids)
                {
                    await Task.Delay(500);
                    LoggerService.Instance.Log($"AddLiquidsToInventory {liquidToAdd.Name} {liquidToAdd.Volume} ml");

                    if (_liquids.ContainsKey(liquidToAdd.Name))
                    {
                        _liquids[liquidToAdd.Name].Volume += liquidToAdd.Volume;
                    }
                    else
                    {
                        _liquids[liquidToAdd.Name] = new Liquid(liquidToAdd.Name, liquidToAdd.Volume);
                    }
                }
            }
            finally
            {
                _semaphoreSlim.Release(); 
            }
        }

        public async Task TakeAsync(IEnumerable<ILiquid> liquids, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();

            await _semaphoreSlim.WaitAsync(cancellation); 
            try
            {
                foreach (var liquid in liquids)
                {
                    if (!await IsAvailableAsync(liquid, cancellation))
                        throw new ArgumentException($"{liquid.Name} {liquid.Volume} mL doesn't available in Inventory");
                }

                foreach (var liquid in liquids)
                {
                    cancellation.ThrowIfCancellationRequested();
                    await Task.Delay(1000, cancellation);

                    if (!_liquids.TryGetValue(liquid.Name, out var inventoryLiquid))
                    {
                        throw new ArgumentException($"{liquid.Name} doesn't exist in inventory");
                    }

                    inventoryLiquid.Volume -= liquid.Volume;
                    LoggerService.Instance.Log($"Take liquid {liquid.Name} {liquid.Volume} ml");
                }
            }
            finally
            {
                _semaphoreSlim.Release(); 
            }
        }

        private async Task<bool> IsAvailableAsync(ILiquid liquid, CancellationToken cancellation)
        {
            LoggerService.Instance.Log($"Check if {liquid.Name} {liquid.Volume}mL available in Inventory");
            if (liquid.Volume == 0) return true;

            var availableVolume = await GetVolumeAsync(liquid, cancellation);
            return availableVolume >= liquid.Volume;
        }

        private async Task<int> GetVolumeAsync(ILiquid liquid, CancellationToken cancellation)
        {
            await Task.Delay(1000, cancellation);
            if (_liquids.TryGetValue(liquid.Name, out var selectedLiquid))
            {
                return selectedLiquid.Volume;
            }

            throw new ArgumentException($"{liquid.Name} doesn't exist in inventory");
        }

      
    }
}
