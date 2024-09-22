using LiquidMixerApp.Liquids;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Inventory
{
    public interface ILiquidInventory
    {

        Task AddAsync(params Liquid[] liquids);
        Task TakeAsync(params Liquid[] liquids);
        Task<bool> IsAvailableAsync(params Liquid[] liquids);
        Task<int> GetVolumeAsync(Liquid liquid);
        Task <IEnumerable<Liquid>> GetAvailableLiquidsAsync();

    }
}
