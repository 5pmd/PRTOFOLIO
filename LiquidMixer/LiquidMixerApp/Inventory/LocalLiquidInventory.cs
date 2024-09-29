using LiquidMixerApp.Liquids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Inventory
{
    public class LocalLiquidInventory : ILiquidInventory
    {

        private HashSet<Liquid> _liquids = new();
        private object _lock = new object();


        public async Task AddAsync(params Liquid[] liquids)
        {

            foreach (var liquidToAdd in liquids)
            {
                await Task.Delay(1000);
                lock (_lock)
                {
                    if (!_liquids.Add(liquidToAdd))
                    {
                        var currentLiquid = _liquids.First(liquid => liquid == liquidToAdd);
                        currentLiquid.Volume += liquidToAdd.Volume;
                    }
                }

            }
        }

        public async Task<IEnumerable<Liquid>> GetAvailableLiquidsAsync()
        {

            await Task.Delay(1000);
            return _liquids;
        }

        public async Task<int> GetVolumeAsync(Liquid targetLiquid)
        {

            await Task.Delay(1000);
            if (_liquids.TryGetValue(targetLiquid, out var liquid))
            {
                return liquid.Volume;
            }

            return 0;
        }

        public async Task<bool> IsAvailableAsync(params Liquid[] liquids)
        {

            var result = true;

            foreach (var liquid in liquids)
            {
                if (liquid.Volume <= 0) continue;

                var availableVolume = await GetVolumeAsync(liquid);
                if (availableVolume < liquid.Volume) result = false;

            }

            return result;
        }

        public async Task TakeAsync(params Liquid[] liquids)
        {

            foreach (var liquid in liquids)
            {
                if (!await IsAvailableAsync(liquid)) throw new InvalidOperationException($"{liquid.Name} doesn't available in Inventory ");
            }

            foreach (var liquid in liquids)
            {
                _liquids.First(availableLiquid => availableLiquid.Equals(liquid)).Volume -= liquid.Volume;
                Console.WriteLine($"Take liquid {liquid.Name} {liquid.Volume} ml");
            }
        }
    }
}
