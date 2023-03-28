using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    internal interface IServicioSumarTotal
    {
        Task<decimal> SumarTotalGeneral(List<Orders> lOrden);
        Task<decimal> SumarSubTotal(Orders oOrden);

    }
}
