using Api.Documento.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bbl.Implementacion;
using Bbl.Interfaces;

namespace Api.Documento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaProductoController : ControllerBase
    {
        IServicioDocumento _sd;
        public ListaProductoController(IServicioDocumento sd)
        {
            _sd = sd;
        }

        [HttpGet]
        public async Task<IActionResult> GetListaProducto(string fechaDesde, string fechaHasta)
        {
            var listaProducto = await _sd.TraerListaProducto(fechaDesde, fechaHasta);
            if (!listaProducto.Error)
            {
                return File(listaProducto.Pdf, "application/pdf");
            }
            else
            {
                return BadRequest(listaProducto);
            }
        }
    }
}
