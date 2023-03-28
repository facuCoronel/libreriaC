using Api.Producto.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace Api.Producto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        IServicioProducto _sp;

        public ProductoController(IServicioProducto sp)
        {
            _sp = sp;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducto()
        {
            var lProducto = await  _sp.TraerProductos();
            if (!lProducto.Error)
            {
                return Ok(lProducto);
            }
            else return BadRequest(lProducto);
        }

        [HttpPost]
        public async Task<IActionResult> PostProcducto(Product oProducto)
        {
            var insertProducto = await _sp.InsertarProducto(oProducto);
            if (!insertProducto.Error)
            {
                return Ok(insertProducto);
            }
            else return BadRequest(insertProducto);
        }

        [HttpPut]
        public async Task<IActionResult> PutProducto(Product oProducto)
        {
            var actualizarProducto = await _sp.ActualizarProducto(oProducto);
            if (!actualizarProducto.Error)
            {
                return Ok(actualizarProducto);
            }
            else return BadRequest(actualizarProducto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProducto(int idProducto)
        {
            var eliminarProducto = await _sp.EliminarProducto(idProducto);
            if (!eliminarProducto.Error)
            {
                return Ok(eliminarProducto);
            }else return BadRequest(eliminarProducto);
        }
    }
}
