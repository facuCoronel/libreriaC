using Api.Clientes.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace Api.Clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        IServicioCliente _sc;
        public ClienteController(IServicioCliente sc)
        {
            _sc = sc;
        }


        [HttpGet]
        public async Task<IActionResult> GetCliente()
        {
            var lCliente = await _sc.TraerClientes();
            if (!lCliente.Error)
            {
                return Ok(lCliente);
            }else return BadRequest(lCliente);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCliente(int idCliente)
        {
            var deleteCliente = await _sc.EliminarCliente(idCliente);
            if (!deleteCliente.Error)
            {
                return Ok(deleteCliente);
            }
            else return BadRequest(deleteCliente);
        }

        [HttpPost]
       public async Task<IActionResult> PostCliente(Customer oCliente)
        {
            var postCliente = await _sc.InsertarClientes(oCliente);
            if (!postCliente.Error)
            {
                return Ok(postCliente);
            }
            else return BadRequest(postCliente);
        }

        [HttpPut]
        public async Task<IActionResult> PutCliente(Customer oCliente)
        {
            var putCliente = await _sc.ActualizarClientes(oCliente);
            if (!putCliente.Error)
            {
                return Ok(putCliente);
            }
            else return BadRequest(putCliente);
        }
    }
}
