using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    internal interface ICalcularTotal<T>
    {
        Task<decimal?> CalcularSubTotal(List<T> lista);
        Task<decimal?> CalcularTotal(decimal? lista);
    }
}
