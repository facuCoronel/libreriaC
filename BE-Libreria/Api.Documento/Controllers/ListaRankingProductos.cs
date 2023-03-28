using Api.Documento.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bbl.Implementacion;

namespace Api.Documento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaRankingProductos : ControllerBase
    {
        IServicioDocumento _sd;

        public ListaRankingProductos(IServicioDocumento sd)
        {
            _sd = sd;
        }

        [HttpGet]
        public async Task<IActionResult> GetRankingProducto(string fechaDesde, string fechaHasta)
        {
            var listaProducto = await _sd.ProductosMasVendidos(fechaDesde, fechaHasta);

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
