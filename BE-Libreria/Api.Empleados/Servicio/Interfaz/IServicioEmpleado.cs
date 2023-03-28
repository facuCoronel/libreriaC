using AccesoDatos;
using Modelos;

namespace Api.Empleados.Servicio.Interfaz
{
    public interface IServicioEmpleado
    {
        Task<RespuestaApi<Employee>> TraerEmpleados();
        Task<RespuestaApi<Employee>> InsertarEmpleado(Employee oEmpleado);
        Task<RespuestaApi<Employee>> ActualizarEmpleado(Employee oEmpleado);
        Task<RespuestaApi<Employee>> EliminarEmpleado(int idEmpleado);
    }
}
