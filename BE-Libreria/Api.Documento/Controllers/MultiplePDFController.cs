using Api.Documento.Servicio.Implementacion;
using Api.Documento.Servicio.Interfaz;
using Bbl.Implementacion;
using Bbl.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Documento.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultiplePDFController : ControllerBase
    {
        IServicioDocumento _sd;

        public MultiplePDFController(IServicioDocumento sd)
        {
            _sd = sd;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPDFs(bool factura, bool listaProducto, bool RankingEmp, bool RankingProd, string fechaFactura, string FechaDesdeListaProd, string FechaHastaListaProd,string fechaDesdeRanEmp, string fechaHastaRanEmp, string fechaDesdeRanProd, string fechaHastaRanProd)
        {


            var listado = new List<byte[]>();

            if (factura)
            {
                var Factura = await _sd.TraerFacturas(fechaFactura);
                if(!Factura.Error)
                {
                    listado.Add(Factura.Pdf);
                }
                else
                {
                    return BadRequest(Factura);
                }
            }
            if (listaProducto)
            {
                var ListaProducto = await _sd.TraerListaProducto(FechaDesdeListaProd, FechaHastaListaProd);
                if(!ListaProducto.Error)
                {
                    listado.Add(ListaProducto.Pdf);
                }
                else
                {
                    return BadRequest(ListaProducto);
                }
            }
            if (RankingEmp)
            {
                var RanEmp = await _sd.TraerRankingVendedores(fechaDesdeRanEmp, fechaHastaRanEmp);
                if(!RanEmp.Error)
                {
                    listado.Add(RanEmp.Pdf);
                }
                else
                {
                    return BadRequest(RanEmp);
                }
            }
            if(RankingProd)
            {
                var RanProd = await _sd.ProductosMasVendidos(fechaDesdeRanProd, fechaHastaRanProd);
                if(!RanProd.Error)
                {
                    listado.Add(RanProd.Pdf);

                }else
                {
                    return BadRequest(RanProd);
                }
            }

            var resultado = await _sd.MultiplesPDF(listado);

            if (!resultado.Error)
            {
                return File(resultado.Pdf, "application/pdf");
            }
            else
            {
                return BadRequest(resultado);
            }
        }



    }
}
