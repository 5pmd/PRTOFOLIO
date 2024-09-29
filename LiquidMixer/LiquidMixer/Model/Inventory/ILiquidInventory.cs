
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiquidMixerGUI.Model.Inventory
{
    public interface ILiquidInventory
    {

        Task AddAsync(IEnumerable<Liquid> liquids);
        Task TakeAsync(IEnumerable<Liquid> liquids, CancellationToken cancellation);

    }
}
