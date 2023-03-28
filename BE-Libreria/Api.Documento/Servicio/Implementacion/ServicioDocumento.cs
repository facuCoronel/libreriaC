using AccesoDatos;
using Api.Documento.Servicio.Interfaz;
using AppLibreria;
using Modelos;
using Dapper;
using Bbl.Interfaces;
using Newtonsoft.Json;
using System.Data;
using Bbl.Implementacion;

namespace Api.Documento.Servicio.Implementacion
{
    public class ServicioDocumento : IServicioDocumento
    {
        IConfiguration _config;
        IServicioRankingProductoPDF _rankingProd;
        IServicioRankingEmpleadoPDF _rankingEmp;
        IServicioListaProductosPDF _listaProd;
        IServicioFacturaPDF _facturaPDF;
        IServicioAdjuntarPDF _adjuntarPDF;


        public ServicioDocumento(IConfiguration config, IServicioRankingProductoPDF rankingProd, IServicioRankingEmpleadoPDF rankingEmpleado, IServicioListaProductosPDF listaProd, IServicioFacturaPDF facturaPDF, IServicioAdjuntarPDF adjuntarPDF)
        {
            _config = config;
            _rankingProd = rankingProd;
            _rankingEmp = rankingEmpleado;
            _facturaPDF = facturaPDF;
            _listaProd = listaProd;
            _adjuntarPDF = adjuntarPDF;
        }

        public async Task<RespuestaPDF> MultiplesPDF(List<byte[]> Listado)
        {
            var respuestaApi = new RespuestaPDF();
            try
            {

                var pdf = await _adjuntarPDF.AdjuntarPDF(Listado);
                if (respuestaApi.Pdf == null)
                {
                    respuestaApi.Error = false;
                    respuestaApi.Mensaje = "Se realizo la consulta correctamente";
                    respuestaApi.Pdf = pdf;
                    return respuestaApi;
                }
                else
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = "La lista fue null";
                    return respuestaApi;
                }
            }
            catch (Exception ex)
            {
                respuestaApi.Error = true;
                respuestaApi.Mensaje = $"Se produjo una EXCEPCION: {ex}";
                return respuestaApi;
            }
        }

        public async Task<RespuestaPDF> ProductosMasVendidos(string fechaDesde, string fechaHasta)
        {
            var respuestaApi = new RespuestaPDF();
            var urlOrdenes = _config["UrlApiOrdenes"];
            var cliente = new HttpClient();
            var Orders = new Orders();

            try
            {
                var respuestaServicio = await cliente.GetAsync($"{urlOrdenes}?");
                string jsonSerializado = await respuestaServicio.Content.ReadAsStringAsync();
                var lOrden = JsonConvert.DeserializeObject<RespuestaApi<Orders>>(jsonSerializado);
                var lRankingProd = new List<RankingProductosPDF>();

                if (lOrden != null)
                {
                    var fechaDes = Convert.ToDateTime(fechaDesde);
                    var fechahas = Convert.ToDateTime(fechaHasta);

                    foreach (var o in lOrden.Data)
                    {
                        Convert.ToDateTime(o.OrderDate);

                        if(fechaDes < o.OrderDate && fechahas > o.OrderDate)
                        {
                            var oRanking = new RankingProductosPDF();
                            oRanking.Descripcion = o.Product.Name;
                            oRanking.IdProd = o.Product.ProductId;
                            oRanking.Precio = o.Product.Price;
                            oRanking.Cantidad = o.Quantity;
                            lRankingProd.Add(oRanking);
                        }
                    }
                    var result = (from item in lRankingProd
                                  group item by item.Descripcion into g
                                  select new RankingProductosPDF()
                                  {
                                      Descripcion = g.Key,
                                      Cantidad = g.Sum(x => x.Cantidad),
                                      Precio = g.FirstOrDefault().Precio
                                  }).OrderByDescending(x => x.Cantidad).ToList();


                    var pdf = await _rankingProd.ListaProductosTop(result, fechaDesde, fechaHasta);
                    respuestaApi.Error = false;
                    respuestaApi.Mensaje = "Se realizo la consulta correctamente";
                    respuestaApi.Pdf = pdf;
                    return respuestaApi;
                }
                else
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = "La lista fue null";
                    return respuestaApi;
                }
            }catch(Exception ex)
            {
                respuestaApi.Error = true;
                respuestaApi.Mensaje = $"Se produjo una EXCEPCION: {ex}";
                return respuestaApi;
            }
        }

        public async Task<RespuestaPDF> TraerFacturas(string fechaFactura)
        {
            var respuestaApi = new RespuestaPDF();
            var urlOrdenes = _config["UrlApiOrdenes"];
            var cliente = new HttpClient();

            try
            {
                var respuestaServicio = await cliente.GetAsync($"{urlOrdenes}?");
                string jsonSerializado = await respuestaServicio.Content.ReadAsStringAsync();
                var lFactura = JsonConvert.DeserializeObject<RespuestaApi<Orders>>(jsonSerializado);
                var facturas = new List<FacturaPDF>();
                if (lFactura != null)
                {
                    var fechaFac = Convert.ToDateTime(fechaFactura);
                    foreach (var o in lFactura.Data)
                    {
                        Convert.ToDateTime(o.OrderDate);

                        if (o.OrderDate == fechaFac)
                        {
                            var oFactura = new FacturaPDF();
                            oFactura.Descripcion = o.Product.Name;
                            oFactura.Cliente = o.Customer.Name;
                            oFactura.Cantidad = o.Quantity;
                            oFactura.Total = o.Product.Price;
                            oFactura.IdFactura = o.OrderId;
                            facturas.Add(oFactura);
                        }
                    }

                    var result = (from item in facturas
                                 group item by item.IdFactura into g
                                 select new FacturaPDF()
                                 {
                                     IdFactura = g.Key,
                                     Descripcion = g.SingleOrDefault().Descripcion,
                                     Total = g.SingleOrDefault().Total,
                                     Cantidad = g.Sum(x => x.Cantidad),
                                     Cliente = g.SingleOrDefault().Cliente
                                 }).ToList();

                    var pdf = await _facturaPDF.GenerarFactura(result, fechaFactura);
                    respuestaApi.Error = false;
                    respuestaApi.Mensaje = "Se realizo la consulta correctamente";
                    respuestaApi.Pdf = pdf;
                    return respuestaApi;
                }
                else
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = "La lista fue null";
                    return respuestaApi;
                }
            }
            catch (Exception ex)
            {
                respuestaApi.Error = true;
                respuestaApi.Mensaje = $"Se produjo una EXCEPCION: {ex}";
                return respuestaApi;
            }
        }

        public async Task<RespuestaPDF> TraerListaProducto(string fechaDesde, string fechaHasta)
        {
            var respuestaApi = new RespuestaPDF();
            var urlOrdenes = _config["UrlApiOrdenes"];
            var cliente = new HttpClient();

            try
            {
                var respuestaServicio = await cliente.GetAsync($"{urlOrdenes}?");
                string jsonSerializado = await respuestaServicio.Content.ReadAsStringAsync();
                var lOrden = JsonConvert.DeserializeObject<RespuestaApi<Orders>>(jsonSerializado);
                var lProductos = new List<ListaProductoPDF>();
                if (lOrden != null)
                {
                    var fechaDes = Convert.ToDateTime(fechaDesde);
                    var fechahas = Convert.ToDateTime(fechaHasta);
                    foreach (var o in lOrden.Data)
                    {
                        Convert.ToDateTime(o.OrderDate);

                        if (fechaDes < o.OrderDate && fechahas > o.OrderDate)
                        {
                            var oProducto = new ListaProductoPDF();
                            oProducto.Descripcion = o.Product.Name;
                            oProducto.IdProd = o.Product.ProductId;
                            oProducto.Stock = o.Product.Stock;
                            oProducto.Precio = o.Product.Price;
                            lProductos.Add(oProducto);
                        }
                    }

                    var result = (from item in lProductos
                                 group item by item.IdProd into g
                                 select new ListaProductoPDF()
                                 {
                                     IdProd = g.Key,
                                     Descripcion = g.FirstOrDefault().Descripcion,
                                     Stock = g.FirstOrDefault().Stock,
                                     Precio = g.FirstOrDefault().Precio
                                 }).OrderByDescending(x => x.Stock).ToList();

                    var pdf = await _listaProd.GenerarListaProducto(result, fechaDesde, fechaHasta);
                    respuestaApi.Error = false;
                    respuestaApi.Mensaje = "Se realizo la consulta correctamente";
                    respuestaApi.Pdf = pdf;
                    return respuestaApi;
                }
                else
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = "La lista fue null";
                    return respuestaApi;
                }
            }
            catch (Exception ex)
            {
                respuestaApi.Error = true;
                respuestaApi.Mensaje = $"Se produjo una EXCEPCION: {ex}";
                return respuestaApi;
            }
        }

        public async Task<RespuestaPDF> TraerRankingVendedores(string fechaDesde, string fechaHasta)
        {

            var respuestaApi = new RespuestaPDF();
            var urlOrdenes = _config["UrlApiOrdenes"];
            var cliente = new HttpClient();

            try
            {
                var respuestaServicio = await cliente.GetAsync($"{urlOrdenes}?");
                string jsonSerializado = await respuestaServicio.Content.ReadAsStringAsync();
                var lOrden = JsonConvert.DeserializeObject<RespuestaApi<Orders>>(jsonSerializado);
                var lEmpleado = new List<RankingEmpleadoPDF>();
                if (lOrden != null)
                {
                    var fechaDes = Convert.ToDateTime(fechaDesde);
                    var fechahas = Convert.ToDateTime(fechaHasta);
                    foreach (var o in lOrden.Data)
                    {
                        Convert.ToDateTime(o.OrderDate);

                        if (fechaDes < o.OrderDate && fechahas > o.OrderDate)
                        {
                            var oEmpleado = new RankingEmpleadoPDF();
                            oEmpleado.Name = o.Employee.Name;
                            oEmpleado.Surname = o.Employee.Surname;
                            oEmpleado.Quantity = o.OrderId;
                            oEmpleado.IdEmployee = o.Employee.EmployeeId;
                            lEmpleado.Add(oEmpleado);
                        }
                    }

                    var result = (from item in lEmpleado
                                  group item by item.IdEmployee into g
                                  select new RankingEmpleadoPDF()
                                  {
                                      IdEmployee = g.Key,
                                      Name = g.FirstOrDefault().Name,
                                      Surname = g.FirstOrDefault().Surname,
                                      Quantity = g.Sum(x => x.Quantity - x.Quantity + 1)
                                  }).OrderByDescending(x => x.Quantity).ToList();


                    var pdf = await _rankingEmp.GenerarListaEmpleadosTop(result, fechaDesde, fechaHasta);
                    respuestaApi.Error = false;
                    respuestaApi.Mensaje = "Se realizo la consulta correctamente";
                    respuestaApi.Pdf = pdf;
                    return respuestaApi;
                }
                else
                {
                    respuestaApi.Error = true;
                    respuestaApi.Mensaje = "La lista fue null";
                    return respuestaApi;
                }
            }
            catch (Exception ex)
            {
                respuestaApi.Error = true;
                respuestaApi.Mensaje = $"Se produjo una EXCEPCION: {ex}";
                return respuestaApi;
            }
        }


    }
}
