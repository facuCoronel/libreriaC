using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ListaProductoPDF
    {
        public int IdProd { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public decimal Precio { get; set; }
    }
}
