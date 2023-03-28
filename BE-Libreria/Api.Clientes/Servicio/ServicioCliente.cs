using AccesoDatos;
using Api.Clientes.Servicio.Interfaz;
using AppLibreria;
using Dapper;
using Modelos;

namespace Api.Clientes.Servicio
{
    public class ServicioCliente : IServicioCliente
    {
        ConexionBd _bd;

        public ServicioCliente(ConexionBd bd)
        {
            _bd = bd;
        }

        public async Task<RespuestaApi<Customer>> ActualizarClientes(Customer oCliente)
        {
            string query = @"Update Customer set name = @name, email = @email, phone = @phone where CustomerId = @id";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Customer>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new { name = oCliente.Name, email = oCliente.Email, phone = oCliente.Phone, id = oCliente.CustomerId});
                    if (filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se actualizo correctamente el CLIENTE {oCliente.Name}";
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

        public async Task<RespuestaApi<Customer>> EliminarCliente(int idCliente)
        {
            string query = @"Delete from Customer where CustomerId = @id";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Customer>();
                try
                {
                    var filasAfectdas = await conn.ExecuteAsync(query, new { id = idCliente });
                    if (filasAfectdas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se elimino correctamente el CLIENTE con ID = {idCliente}";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = $"NO se elimino correctamente el PRODUCTO con ID = {idCliente}";
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

        public async Task<RespuestaApi<Customer>> InsertarClientes(Customer oCliente)
        {
            string query = @"Insert into Customer values (@name,@email, @phone)";

            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Customer>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new { name = oCliente.Name, email = oCliente.Email, phone = oCliente.Phone });
                    if (filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se inserto {oCliente.Name} CORRECTAMENTE ";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = "No se pudo insertar ningun CLIENTE, intente nuevamente";
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

        public async Task<RespuestaApi<Customer>> TraerClientes()
        {
            string query = "Select * from Customer";

            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Customer>();
                try
                {
                    var lCliente = (await conn.QueryAsync<Customer>(query)).ToList();

                    if (lCliente.Count == 0)
                    {
                        respuestaApi.Data = null;
                        respuestaApi.Mensaje = "No se encontro ningun CLIENTE";
                        respuestaApi.Error = true;
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Data = lCliente;
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
