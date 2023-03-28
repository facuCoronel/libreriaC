using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos
{
    public class RespuestaApi<T> : RespuestaBase
    {
       public List<T>? Data { get; set; } 
    }
    public class RespuestaApi : RespuestaBase
    {

    }
    public class RespuestaBase
    {
        public bool Error { get; set; }
        public string? Mensaje { get; set; }
    }
    public class RespuestaPDF : RespuestaBase
    {
        public byte[] Pdf { get; set; } 
    }
}
