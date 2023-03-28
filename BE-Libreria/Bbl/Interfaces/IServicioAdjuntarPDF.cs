using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    public interface IServicioAdjuntarPDF
    {
        Task<byte[]> AdjuntarPDF(List<byte[]> pdf);
    }
}
