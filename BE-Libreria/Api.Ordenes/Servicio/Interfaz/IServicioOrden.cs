using AccesoDatos;
using Modelos;
using Bbl.Implementacion;

namespace Api.Ordenes.Servicio.Interfaz
{
    public interface IServicioOrden
    {
        Task<RespuestaApi<Orders>> TraerOrden();
        Task<RespuestaApi<Orders>> InsertarOrden(ImplementacionListaOrden listaOrden);
        Task<RespuestaApi<Orders>> ActualizarOrden(Orders oOrden);
        Task<RespuestaApi<Orders>> EliminarOrden(int idOrden);
    }
}
