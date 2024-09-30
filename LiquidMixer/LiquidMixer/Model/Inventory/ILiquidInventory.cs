
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerApp.Model.Inventory
{
    public interface ILiquidInventory
    {

        Task AddAsync(IEnumerable<ILiquid> liquids);
        Task TakeAsync(IEnumerable<ILiquid> liquids, CancellationToken cancellation);


    }
}
