using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    internal interface IServicioTablaResumen<T>
    {
        Task<Table> CrearTablaResumen(List<T> lista);
    }
}
