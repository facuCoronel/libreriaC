using AccesoDatos;
using Modelos;

namespace Api.Producto.Servicio.Interfaz
{
    public interface IServicioProducto
    {
        Task<RespuestaApi<Product>> TraerProductos(); 
        Task<RespuestaApi<Product>> InsertarProducto(Product oProducto);
        Task<RespuestaApi<Product>> ActualizarProducto(Product oProducto);
        Task<RespuestaApi<Product>> EliminarProducto(int idProducto);
    }
}
