using Bbl.Interfaces;
using iText.Layout.Element;
using Modelos;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    internal class ImplementacionTablaFactura : IServicioTabla<FacturaPDF>, IServicioTablaResumen<FacturaPDF>
    {
        public async Task<Table> CrearTabla(List<FacturaPDF> list)
        {
            Table maestro = new Table(4).SetHorizontalAlignment(HorizontalAlignment.CENTER);
            maestro.SetWidth(500);
            maestro.SetBorderBottom(new SolidBorder(2));
            maestro.SetBorderTop(new SolidBorder(1));

            float fontSize = 15;


            //Encabezado de la tabla
            Cell cellDescripcion = new Cell();
            cellDescripcion.Add(new Paragraph("Descripcion"));
            cellDescripcion.SetBorder(Border.NO_BORDER);
            cellDescripcion.SetBorderBottom(new SolidBorder(2));
            cellDescripcion.SetFontSize(fontSize);
            cellDescripcion.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellDescripcion.SetFontColor(ColorConstants.DARK_GRAY);


            Cell cellPrecio = new Cell();
            cellPrecio.Add(new Paragraph("Precio "));
            cellPrecio.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellPrecio.SetFontSize(fontSize);
            cellPrecio.SetBorder(Border.NO_BORDER);
            cellPrecio.SetTextAlignment(TextAlignment.CENTER);
            cellPrecio.SetBorderBottom(new SolidBorder(2));
            cellPrecio.SetFontColor(ColorConstants.DARK_GRAY);

            Cell cellCantidad = new Cell();
            cellCantidad.Add(new Paragraph("Cantidad "));
            cellCantidad.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellCantidad.SetFontSize(fontSize);
            cellCantidad.SetBorder(Border.NO_BORDER);
            cellCantidad.SetTextAlignment(TextAlignment.CENTER);
            cellCantidad.SetBorderBottom(new SolidBorder(2));
            cellCantidad.SetFontColor(ColorConstants.DARK_GRAY);

            Cell cellTotal = new Cell();
            cellTotal.Add(new Paragraph("Total "));
            cellTotal.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellTotal.SetFontSize(fontSize);
            cellTotal.SetBorder(Border.NO_BORDER);
            cellTotal.SetTextAlignment(TextAlignment.CENTER);
            cellTotal.SetBorderBottom(new SolidBorder(2));
            cellTotal.SetFontColor(ColorConstants.DARK_GRAY);

            maestro.AddHeaderCell(cellDescripcion);
            maestro.AddHeaderCell(cellPrecio);
            maestro.AddHeaderCell(cellCantidad);
            maestro.AddHeaderCell(cellTotal);

            //Detalle de la tabla
            foreach (var item in list)
            {
                Cell cellContenidoDescrip = new Cell();
                cellContenidoDescrip.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                cellContenidoDescrip.SetFontSize(fontSize);
                cellContenidoDescrip.Add(new Paragraph(item.Descripcion));
                cellContenidoDescrip.SetBorder(Border.NO_BORDER);
                cellContenidoDescrip.SetTextAlignment(TextAlignment.LEFT);
                cellContenidoDescrip.SetFontColor(ColorConstants.DARK_GRAY);

                Cell cellContenidoPrecio = new Cell();
                cellContenidoPrecio.Add(new Paragraph("$ " + Convert.ToString(item.Total)));
                cellContenidoPrecio.SetFontSize(fontSize);
                cellContenidoPrecio.SetBorder(Border.NO_BORDER);
                cellContenidoPrecio.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                cellContenidoPrecio.SetTextAlignment(TextAlignment.CENTER);
                cellContenidoPrecio.SetFontColor(ColorConstants.DARK_GRAY); 
                
                Cell cellContenidoCant = new Cell();
                cellContenidoCant.Add(new Paragraph(Convert.ToString(item.Cantidad)));
                cellContenidoCant.SetBorder(Border.NO_BORDER);
                cellContenidoCant.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                cellContenidoCant.SetFontSize(fontSize);
                cellContenidoCant.SetTextAlignment(TextAlignment.CENTER);
                cellContenidoCant.SetFontColor(ColorConstants.DARK_GRAY);
                
                Cell cellContenidoTotal = new Cell();
                cellContenidoTotal.Add(new Paragraph("$ "+Convert.ToString(item.Cantidad * item.Total)));
                cellContenidoTotal.SetBorder(Border.NO_BORDER);
                cellContenidoTotal.SetFontSize(fontSize);
                cellContenidoTotal.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                cellContenidoTotal.SetTextAlignment(TextAlignment.CENTER);
                cellContenidoTotal.SetFontColor(ColorConstants.DARK_GRAY);

                maestro.AddCell(cellContenidoDescrip);
                maestro.AddCell(cellContenidoPrecio);
                maestro.AddCell(cellContenidoCant);
                maestro.AddCell(cellContenidoTotal);
            }

            return maestro;
        }

        public async Task<Table> CrearTablaResumen(List<FacturaPDF> lista)
        {
            float fontSizeResumen = 17;
            var calcularTotal = new ImplementacionCalcularTotal();
            decimal? subtotal = await calcularTotal.CalcularSubTotal(lista);
            decimal? Total = await calcularTotal.CalcularTotal(subtotal);
            Table totales = new Table(2, false).SetHorizontalAlignment(HorizontalAlignment.RIGHT);
            totales.SetWidth(240);

            Cell cellSubTotal = new Cell();
            cellSubTotal.Add(new Paragraph("SubTotal ").SetFontSize(fontSizeResumen));
            cellSubTotal.SetBorder(Border.NO_BORDER);
            cellSubTotal.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellSubTotal.SetTextAlignment(TextAlignment.LEFT);
            cellSubTotal.SetFontColor(ColorConstants.DARK_GRAY);
            totales.AddHeaderCell(cellSubTotal);

            Cell cellCalculoSubTotal = new Cell();
            cellCalculoSubTotal.Add(new Paragraph("$ " + Convert.ToString(subtotal)).SetFontSize(fontSizeResumen));
            cellCalculoSubTotal.SetBorder(Border.NO_BORDER);
            cellCalculoSubTotal.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellCalculoSubTotal.SetTextAlignment(TextAlignment.CENTER);
            cellCalculoSubTotal.SetFontColor(ColorConstants.DARK_GRAY);
            totales.AddHeaderCell(cellCalculoSubTotal);

            decimal impuesto = 10;
            Cell cellImpuesto = new Cell();
            cellImpuesto.Add(new Paragraph($"Impuestos {impuesto} %").SetFontSize(fontSizeResumen));
            cellImpuesto.SetBorder(Border.NO_BORDER);
            cellImpuesto.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellImpuesto.SetTextAlignment(TextAlignment.LEFT);
            cellImpuesto.SetFontColor(ColorConstants.DARK_GRAY);
            totales.AddCell(cellImpuesto);

            decimal? CalculoImpuesto = Total - subtotal;
            Cell cellCalculoImpuesto = new Cell();
            cellCalculoImpuesto.Add(new Paragraph("$ " + Convert.ToString(CalculoImpuesto)).SetFontSize(fontSizeResumen));
            cellCalculoImpuesto.SetBorder(Border.NO_BORDER);
            cellCalculoImpuesto.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellCalculoImpuesto.SetTextAlignment(TextAlignment.CENTER);
            cellCalculoImpuesto.SetFontColor(ColorConstants.DARK_GRAY);
            totales.AddCell(cellCalculoImpuesto);


            Cell cellTotal = new Cell();
            cellTotal.Add(new Paragraph("Total ").SetFontSize(fontSizeResumen));
            cellTotal.SetBorder(Border.NO_BORDER);
            cellTotal.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellTotal.SetTextAlignment(TextAlignment.LEFT);
            cellTotal.SetFontColor(ColorConstants.DARK_GRAY);
            totales.AddCell(cellTotal);

            Cell cellCalculoTotal = new Cell();
            cellCalculoTotal.Add(new Paragraph("$ " + Convert.ToString(Total)).SetFontSize(fontSizeResumen));
            cellCalculoTotal.SetBorder(Border.NO_BORDER);
            cellCalculoTotal.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellCalculoTotal.SetTextAlignment(TextAlignment.CENTER);
            cellCalculoTotal.SetFontColor(ColorConstants.DARK_GRAY);
            totales.AddCell(cellCalculoTotal);

            totales.SetBorderBottom(new SolidBorder(2));

            return totales;
        }
    }
}
