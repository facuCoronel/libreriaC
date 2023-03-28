using Api.Documento.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using Bbl.Interfaces;
using Bbl.Implementacion;

namespace Api.Documento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        IServicioDocumento _sd;


        public FacturaController(IServicioDocumento sd)
        {
            _sd = sd;

        }

        [HttpGet]
        public async Task<IActionResult> GetFactura(string fecha)
        {
            var factura = await _sd.TraerFacturas(fecha);
            int clave = 0;
            if(!factura.Error)
            {

                return File(factura.Pdf, "application/pdf");
            }
            else
            {
                return BadRequest(factura);
            }
        }

        //[HttpPut("ListaProducto")]
        //public async Task<IActionResult> PutFactura(string fechaDesde, string fechaHasta)
        //{
        //    var listaProducto = await _sd.TraerListaProducto(fechaDesde, fechaHasta);
        //    if (!listaProducto.Error)
        //    {
        //        await _ap.AgregarPDF(listaProducto.Pdf, 1);
        //        return File(listaProducto.Pdf, "application/pdf");
        //    }
        //    else
        //    {
        //        return BadRequest(listaProducto);
        //    }
        //}

        //[HttpDelete("EmpleadosTop")]
        //public async Task<IActionResult> DeleteFactura(string fechaDesde, string fechaHasta)
        //{
        //    var listaEmpleado = await _sd.TraerRankingVendedores(fechaDesde, fechaHasta);
        //    if (!listaEmpleado.Error)
        //    {
        //        await _ap.AgregarPDF(listaEmpleado.Pdf, 2);
        //        return File(listaEmpleado.Pdf, "application/pdf");
        //    }
        //    else
        //    {
        //        return BadRequest(listaEmpleado);
        //    }
        //}

        //[HttpPost("RankingProductos")]
        //public async Task<IActionResult> PostFactura(string fechaDesde, string fechaHasta)
        //{
        //    var listaProducto = await _sd.ProductosMasVendidos(fechaDesde, fechaHasta);

        //    if (!listaProducto.Error)
        //    {
        //        await _ap.AgregarPDF(listaProducto.Pdf, 3);
        //        return File(listaProducto.Pdf, "application/pdf");
        //    }
        //    else
        //    {
        //        return BadRequest(listaProducto);
        //    }

        //}

    }
}
