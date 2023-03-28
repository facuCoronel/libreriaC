using Bbl.Interfaces;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    public class ImplementacionSumarTotal : IServicioSumarTotal
    {
        ImplementacionListaOrden? sumarOrden { get; set; }
        private decimal Total { get; set; }
        private decimal SubTotal { get; set; }

        public ImplementacionSumarTotal()
        {
            sumarOrden = new ImplementacionListaOrden();
        }

        public async Task<decimal> SumarTotalGeneral(List<Orders> lOrden)
        {
            foreach(var item in sumarOrden.lOrden)
            {
                Total += item.Product.Price * item.Quantity;
            }
            return Total;
        }

        public async Task<decimal> SumarSubTotal(Orders oOrden)
        {
            SubTotal = oOrden.Product.Price * oOrden.Quantity;
            return SubTotal;
        }
    }
}
