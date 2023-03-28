
using Api.Empleados.Servicio.Interfaz;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos;

namespace Api.Empleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        IServicioEmpleado _se;
        public EmpleadoController(IServicioEmpleado se)
        {
            _se = se;
        }


        [HttpGet]
        public async  Task<IActionResult> GetEmpleado()
        {
            var lEmpleado = await _se.TraerEmpleados();
            if(lEmpleado.Error)
            {
                return Ok(lEmpleado);
            }
            else
            {
                return BadRequest(lEmpleado);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostEmpleado(Employee oEmpleado)
        {
            var postEmpleado = await _se.InsertarEmpleado(oEmpleado);

            if (!postEmpleado.Error)
            {
                return Ok(postEmpleado);
            }
            else return BadRequest(postEmpleado);
        }

        [HttpPut]
        public async Task<IActionResult> PutEmpleado(Employee oEmpleado)
        {
            var putEmpleado = await _se.ActualizarEmpleado(oEmpleado);
            if(!putEmpleado.Error)
            {
                return Ok(putEmpleado);
            }
            else return BadRequest(putEmpleado);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEmpleado(int idEmpleado)
        {
            var deleteEmpleado = await _se.EliminarEmpleado(idEmpleado);
            if (deleteEmpleado.Error)
            {
                return Ok(deleteEmpleado);
            }
            else return BadRequest(deleteEmpleado);
        }

    }
}
