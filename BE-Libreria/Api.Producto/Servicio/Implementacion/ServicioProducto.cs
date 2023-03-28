using AccesoDatos;
using Api.Producto.Servicio.Interfaz;
using AppLibreria;
using Dapper;
using Modelos;

namespace Api.Producto.Servicio.Implementacion
{
    public class ServicioProducto : IServicioProducto
    {
        ConexionBd _bd;
        public ServicioProducto(ConexionBd bd)
        {
            _bd = bd;
        }

        public async Task<RespuestaApi<Product>> ActualizarProducto(Product oProducto)
        {
            string query = @"Update Product set name = @name, stock = @stock, price = @price where ProductId = @id";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Product>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new { name = oProducto.Name, stock = oProducto.Stock, price = oProducto.Price, id = oProducto.ProductId });
                    if (filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se actualizo correctamente el PRODUCTO con ID = {oProducto.ProductId}";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = "No se pudo actualizar, intente nuevamente";
                        return respuestaApi;
                    }

                }
                catch (Exception ex)
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = $"No se pudo actualizar por {ex}";
                    return respuestaApi;
                }
            }
        }

        public async Task<RespuestaApi<Product>> EliminarProducto(int idProducto)
        {
            string query = @"Delete from Product where ProductId = @id";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Product>();
                try
                {
                    var filasAfectdas = await conn.ExecuteAsync(query, new { id = idProducto });
                    if (filasAfectdas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se elimino correctamente el PRODUCTO con ID = {idProducto}";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = $"NO se elimino correctamente el PRODUCTO con ID = {idProducto}";
                        return respuestaApi;
                    }
                }
                catch (Exception ex)
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = $"Se produjo el error {ex}";
                    return respuestaApi;
                }
            }
        }

        public async Task<RespuestaApi<Product>> InsertarProducto(Product oProducto)
        {
            string query = @"Insert into Product values (@name,@stock, @price)";

            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Product>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new { name = oProducto.Name, stock = oProducto.Stock, price = oProducto.Price });
                    if (filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se inserto {oProducto.Name} CORRECTAMENTE ";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = "No se pudo insertar ningun PRODUCTO, intente nuevamente";
                        return respuestaApi;
                    }
                }
                catch (Exception ex)
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = $"Ocurrio el siguiente problema {ex}";
                    return respuestaApi;
                }
            }
        }

        public async Task<RespuestaApi<Product>> TraerProductos()
        {
            string query = "Select * from product";

            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Product>();
                try
                {
                    var lProductos = (await conn.QueryAsync<Product>(query)).ToList();

                    if (lProductos.Count == 0)
                    {
                        respuestaApi.Data = null;
                        respuestaApi.Mensaje = "No se encontro ningun PRODUCTO";
                        respuestaApi.Error = true;
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Data = lProductos;
                        respuestaApi.Mensaje = "Ok";
                        respuestaApi.Error = false;
                        return respuestaApi;
                    }
                }
                catch (Exception ex)
                {
                    respuestaApi.Data = null;
                    respuestaApi.Mensaje = $"Error: {ex}";
                    respuestaApi.Error = true;
                    return respuestaApi;
                }
            }
        }



    }
}
