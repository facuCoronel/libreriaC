using AccesoDatos;
using Modelos;

namespace Api.Documento.Servicio.Interfaz
{
    public interface IServicioDocumento
    {
        Task<RespuestaPDF> TraerListaProducto(string fechaDesde, string fechaHasta);
        Task<RespuestaPDF> ProductosMasVendidos(string fechaDesde, string fechaHasta);
        Task<RespuestaPDF> TraerRankingVendedores(string fechaDesde, string fechaHasta);
        Task<RespuestaPDF> TraerFacturas(string fechaFactura);
        Task<RespuestaPDF> MultiplesPDF(List<byte[]> Listado);
        //Task<RespuestaPDF> GenerarMultiplesPDF(bool factura, bool listaProd, bool ProductosTop, bool EmpleadoTop);


    }
}
