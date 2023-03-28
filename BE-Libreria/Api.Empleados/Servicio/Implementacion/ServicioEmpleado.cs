using AccesoDatos;
using Api.Empleados.Servicio.Interfaz;
using AppLibreria;
using Dapper;
using Modelos;

namespace Api.Empleados.Servicio.Implementacion
{
    public class ServicioEmpleado : IServicioEmpleado
    {
        ConexionBd _bd;
        public ServicioEmpleado(ConexionBd bd)
        {
            _bd = bd;
        }

        public async Task<RespuestaApi<Employee>> ActualizarEmpleado(Employee oEmpleado)
        {
            string query = @"Update Employee set name = @name, surname = @surname where EmployeeID = @id" ;
            using(var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Employee>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new {name = oEmpleado.Name, surname = oEmpleado.Surname, id = oEmpleado.EmployeeId});
                    if(filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se actualizo correctamente el empleado con ID = {oEmpleado.EmployeeId}";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = "No se pudo actualizar, intente nuevamente";
                        return respuestaApi;
                    }
                
                }
                catch(Exception ex)
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = $"No se pudo actualizar por {ex}";
                    return respuestaApi;
                }
            }
        }

        public async Task<RespuestaApi<Employee>> EliminarEmpleado(int idEmpleado)
        {
            string query = @"Delete from Employee where EmployeeID = @id";
            using(var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Employee>();
                try
                {
                    var filasAfectdas = await conn.ExecuteAsync(query, new { id = idEmpleado });
                    if(filasAfectdas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se elimino correctamente el EMPLEADO con ID = {idEmpleado}";
                        return respuestaApi;
                    }else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = $"NO se elimino correctamente el EMPLEADO con ID = {idEmpleado}";
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

        public async Task<RespuestaApi<Employee>> InsertarEmpleado(Employee oEmpleado)
        {
            string query = @"Insert into Employee values (@name,@surname)";

            using(var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Employee>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new { name = oEmpleado.Name, surname = oEmpleado.Surname });
                    if(filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se inserto {oEmpleado.Surname}, {oEmpleado.Name} CORRECTAMENTE ";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = "No se pudo insertar ninguna persona, intente nuevamente";
                        return respuestaApi;
                    }
                }
                catch(Exception ex)
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = $"Ocurrio el siguiente problema {ex}";
                    return respuestaApi;
                }
            }
        }

        public async Task<RespuestaApi<Employee>> TraerEmpleados()
        {
            string query = "Select * from employee";

            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Employee>();
                try
                {
                    var lEmpleados = (await conn.QueryAsync<Employee>(query)).ToList();

                    if (lEmpleados.Count == 0)
                    {
                        respuestaApi.Data = null;
                        respuestaApi.Mensaje = "No se encontro ningun EMPLEADO";
                        respuestaApi.Error = true;
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Data = lEmpleados;
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
