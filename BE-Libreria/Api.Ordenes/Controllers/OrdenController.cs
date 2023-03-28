using Api.Ordenes.Servicio.Interfaz;
using Bbl.Implementacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace Api.Ordenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        IServicioOrden _so;

        public OrdenController(IServicioOrden so)
        {
            _so = so;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrden()
        {
            var obtenerOrden = await _so.TraerOrden();
            if (!obtenerOrden.Error)
            {
                return Ok(obtenerOrden);
            }
            else return BadRequest(obtenerOrden);
        }
            
        [HttpPost]
        public async Task<IActionResult> PostOrden()
        {
            Orders oOrden = new Orders();
            ImplementacionListaOrden listaOrden = new ImplementacionListaOrden();
            oOrden = new Orders();
            oOrden.OrderDate = DateTime.Now;
            oOrden.Product.ProductId = 1;
            oOrden.Customer.CustomerId = 1;
            oOrden.Quantity = 4;
            oOrden.Product.Price = 50;
            oOrden.Employee.EmployeeId = 1;

            await listaOrden.AgregarListaOrden(oOrden);

            oOrden = new Orders();
            oOrden.OrderDate = DateTime.Now;
            oOrden.Product.ProductId = 2;
            oOrden.Customer.CustomerId = 1;
            oOrden.Quantity = 5;
            oOrden.Product.Price = 50;
            oOrden.Employee.EmployeeId = 1;

            await listaOrden.AgregarListaOrden(oOrden);

            var insertarOrder = await _so.InsertarOrden(listaOrden);
            if (!insertarOrder.Error)
            {
                return Ok(insertarOrder);
            }
            else return BadRequest(insertarOrder);
        }

        [HttpPut]
        public async Task<IActionResult> PutOrden(Orders oOrden)
        {
            var actualizarOrden = await _so.ActualizarOrden(oOrden);
            if (!actualizarOrden.Error)
            {
                return Ok(actualizarOrden);
            }
            else return BadRequest(actualizarOrden);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrden(int index)
        {
            var eliminarOrden = await _so.EliminarOrden(index);
            if (!eliminarOrden.Error)
            {
                return Ok(eliminarOrden);
            }
            else return BadRequest(eliminarOrden);
        }
    }
}
