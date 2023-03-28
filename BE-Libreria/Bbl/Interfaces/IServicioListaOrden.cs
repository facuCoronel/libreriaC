using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    internal interface IServicioListaOrden
    {
        Task AgregarListaOrden(Orders oOrden);
        Task RemoverListaOrden(int index);
    }
}
