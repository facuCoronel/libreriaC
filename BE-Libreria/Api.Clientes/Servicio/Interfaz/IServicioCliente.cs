using AccesoDatos;
using Modelos;

namespace Api.Clientes.Servicio.Interfaz
{
    public interface IServicioCliente
    {
        Task<RespuestaApi<Customer>> TraerClientes();
        Task<RespuestaApi<Customer>> InsertarClientes(Customer oCliente);
        Task<RespuestaApi<Customer>> ActualizarClientes(Customer oCliente);
        Task<RespuestaApi<Customer>> EliminarCliente(int idCliente);
    }
}
