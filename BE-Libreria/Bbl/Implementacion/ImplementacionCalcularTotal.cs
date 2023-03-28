using Bbl.Interfaces;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    internal class ImplementacionCalcularTotal : ICalcularTotal<FacturaPDF>
    {
        public ImplementacionCalcularTotal()
        {

        }
        public async Task<decimal?> CalcularSubTotal(List<FacturaPDF> lista)
        {
            decimal? subtotal = 0;  

            foreach(var item in lista)
            {
                subtotal += item.Cantidad * item.Total;
            }

            return subtotal;
            
        }

        public async Task<decimal?> CalcularTotal(decimal? subtotal)
        {
            decimal? total = subtotal * 1.1m;
            return total;
            
        }
    }
}
