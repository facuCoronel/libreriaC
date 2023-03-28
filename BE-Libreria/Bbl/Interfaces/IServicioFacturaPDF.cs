using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Interfaces
{
    public interface IServicioFacturaPDF
    {
        Task<byte[]> GenerarFactura(List<FacturaPDF> lFactura, string fecha);
    }
}
