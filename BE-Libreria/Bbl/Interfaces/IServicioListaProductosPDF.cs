using iText.Layout.Element;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    public interface IServicioListaProductosPDF
    {
        Task<byte[]> GenerarListaProducto(List<ListaProductoPDF> lOrden, string fechaDesde, string fechaHasta);
    }
}
