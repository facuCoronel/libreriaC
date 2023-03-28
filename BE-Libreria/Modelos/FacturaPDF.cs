using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class FacturaPDF
    {
        public int IdFactura { get; set; }
        public string Descripcion { get; set; }
        public string Cliente { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
    }
}
