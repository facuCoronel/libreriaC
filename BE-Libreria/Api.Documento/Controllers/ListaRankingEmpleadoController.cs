using Api.Documento.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bbl.Implementacion;
using Bbl.Interfaces;
namespace Api.Documento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListaRankingEmpleadoController : ControllerBase
    {
        IServicioDocumento _sd;

        public ListaRankingEmpleadoController(IServicioDocumento sd)
        {
            _sd = sd;
        }

        [HttpGet]
        public async Task<IActionResult> GetRankingEmpleados(string fechaDesde, string fechaHasta)
        {
            var listaEmpleado= await _sd.TraerRankingVendedores(fechaDesde, fechaHasta);
            if (!listaEmpleado.Error)
            {
                return File(listaEmpleado.Pdf, "application/pdf");
            }
            else
            {
                return BadRequest(listaEmpleado);
            }
        }


    }
}
