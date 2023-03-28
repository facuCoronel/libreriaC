using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    public interface IServicioRankingEmpleadoPDF
    {
        Task<byte[]> GenerarListaEmpleadosTop(List<RankingEmpleadoPDF> rankingEmpleadoPDFs, string fechaDesde, string fechaHasta);
    }
}
