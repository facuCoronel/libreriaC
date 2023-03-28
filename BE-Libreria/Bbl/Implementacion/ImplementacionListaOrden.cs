using Bbl.Interfaces;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    public class ImplementacionListaOrden : IServicioListaOrden
    {
        public Orders? oOrden { get; set; }
        public List<Orders> lOrden { get; set; }

        public ImplementacionListaOrden()
        {
            oOrden = new Orders();
            lOrden = new List<Orders>();
        }

        public Task AgregarListaOrden(Orders oOrden)
        {
            lOrden.Add(oOrden);
            return Task.CompletedTask;
        }

        public Task RemoverListaOrden(int index)
        {
            lOrden.RemoveAt(index);
            return Task.CompletedTask;
        }
    }
}
