using iText.Layout.Element;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    internal interface IServicioTabla<T>
    {
        Task<Table> CrearTabla(List<T> list);
    }
}
