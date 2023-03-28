using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    public interface IServicioRankingProductoPDF
    {
        Task<byte[]> ListaProductosTop(List<RankingProductosPDF> lOrden, string fechaDesde, string fechaHasta);
    }
}
