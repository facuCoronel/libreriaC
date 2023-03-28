using AccesoDatos;
using Api.Ordenes.Servicio.Interfaz;
using AppLibreria;
using Dapper;
using Modelos;
using Bbl;
using System.Transactions;
using Bbl.Implementacion;
using System.Data.SqlClient;

namespace Api.Ordenes.Servicio.Implementacion
{
    public class ServicioOrden : IServicioOrden
    {
        ConexionBd _bd;

        public ServicioOrden(ConexionBd bd)
        {
            _bd = bd;
        }

        public async Task<RespuestaApi<Orders>> ActualizarOrden(Orders oOrden)
        {
            string query = @"Update Employee set orderDate = @date, CustomerId = @customerId, productID = @productoId, Quantity = @Quantity, TotalPrice = @totalPrice, EmployeeId = @EmployeeId where orderid = @id";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Orders>();
                try
                {
                    var filasAfectadas = await conn.ExecuteAsync(query, new { date = oOrden.OrderDate, customerId = oOrden.Customer.CustomerId, productoId = oOrden.Product.ProductId, Quantity = oOrden.Quantity, totalPrice = oOrden.TotalPrice, EmployeeId = oOrden.Employee.EmployeeId, id= oOrden.OrderId });
                    if (filasAfectadas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se actualizo correctamente el empleado con ID = {oOrden.OrderId}";
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

        public async Task<RespuestaApi<Orders>> EliminarOrden(int idOrden)
        {
            string query = @"Delete from Orders where OrderID = @id";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Orders>();
                try
                {
                    var filasAfectdas = await conn.ExecuteAsync(query, new { id = idOrden});
                    if (filasAfectdas != 0)
                    {
                        respuestaApi.Error = false;
                        respuestaApi.Mensaje = $"Se elimino correctamente la ORDEN con ID = {idOrden}";
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = $"NO se elimino correctamente el ORDEN con ID = {idOrden}";
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

        public async Task<RespuestaApi<Orders>> InsertarOrden(ImplementacionListaOrden listaOrden)
        {
            string query = @"Insert into Orders values (@date, @customerId, @productoId, @Quantity, @totalPrice, @EmployeeId)";
            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Orders>();
                conn.Open();

                var oTotal = new ImplementacionSumarTotal();
                int filasAfectadas = 0;
                decimal totalGeneral = 0;

                using(var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in listaOrden.lOrden)
                        {
                            filasAfectadas += await conn.ExecuteAsync(query, new { date = item.OrderDate, customerId = item.Customer.CustomerId, productoId = item.Product.ProductId, Quantity = item.Quantity, totalPrice = await oTotal.SumarSubTotal(item), EmployeeId = item.Employee.EmployeeId }, transaction: transaction);
                        }
                        totalGeneral = await oTotal.SumarTotalGeneral(listaOrden.lOrden);
                        if (filasAfectadas != 0)
                        {
                            transaction.Commit();
                            respuestaApi.Error = false;
                            respuestaApi.Mensaje = $"Se inserto la ORDEN CORRECTAMENTE ";
                            return respuestaApi;
                        }
                        else
                        {
                            transaction.Rollback();
                            respuestaApi.Mensaje = "No se pudo insertar ninguna persona, intente nuevamente";
                            respuestaApi.Error = true;
                            return respuestaApi;
                        }

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        respuestaApi.Error = true;
                        respuestaApi.Mensaje = $"Ocurrio el siguiente problema {ex}";
                        return respuestaApi;
                    }
                }
            }
        }

        public async Task<RespuestaApi<Orders>> TraerOrden()
        {
            string query = "Select * from orders o join employee e on o.employeeId = e.employeeId join " +
                "           product p on o.productId = p.productId join customer c on o.customerId = c.CustomerId";

            using (var conn = _bd.ConectarBd())
            {
                var respuestaApi = new RespuestaApi<Orders>();
                try
                {
                    var lOrder = (await conn.QueryAsync<Orders, Employee, Product, Customer, Orders>(query, (orden, emp, prod,cli) =>
                    {
                        orden.Employee = emp;
                        orden.Customer = cli;
                        orden.Product = prod;
                        return orden;
                    }
                    ,splitOn: "employeeId, productId, customerId")).ToList();

                    if (lOrder.Count == 0)
                    {
                        respuestaApi.Data = null;
                        respuestaApi.Mensaje = "No se encontro ningun EMPLEADO";
                        respuestaApi.Error = true;
                        return respuestaApi;
                    }
                    else
                    {
                        respuestaApi.Data = lOrder;
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
