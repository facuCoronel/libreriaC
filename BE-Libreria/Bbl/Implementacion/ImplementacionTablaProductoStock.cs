using Bbl.Interfaces;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    internal class ImplementacionTablaProductoStock : IServicioTabla<ListaProductoPDF>
    {
        public async Task<Table> CrearTabla(List<ListaProductoPDF> list)
        {
            //Tabla
            Table maestro = new Table(3, false).SetHorizontalAlignment(HorizontalAlignment.CENTER);
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
            cellPrecio.Add(new Paragraph("Precio"));
            cellPrecio.SetBorder(Border.NO_BORDER);
            cellPrecio.SetBorderBottom(new SolidBorder(2));
            cellPrecio.SetFontSize(fontSize);
            cellPrecio.SetTextAlignment(TextAlignment.CENTER);
            cellPrecio.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellPrecio.SetFontColor(ColorConstants.DARK_GRAY);


            Cell cellCantidad = new Cell();
            cellCantidad.Add(new Paragraph("Cantidad"));
            cellCantidad.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
            cellCantidad.SetBorderBottom(new SolidBorder(2));
            cellCantidad.SetFontSize(fontSize);
            cellCantidad.SetTextAlignment(TextAlignment.CENTER);
            cellCantidad.SetBorder(Border.NO_BORDER);
            cellCantidad.SetFontColor(ColorConstants.DARK_GRAY);

            maestro.AddHeaderCell(cellDescripcion);
            maestro.AddHeaderCell(cellPrecio);
            maestro.AddHeaderCell(cellCantidad);

            //Detalle de la tabla
            foreach (var item in list)
            {

                if (item.Stock == 1)
                {
                    Cell cellContenidoDescrip = new Cell();
                    cellContenidoDescrip.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    cellContenidoDescrip.Add(new Paragraph(item.Descripcion));
                    cellContenidoDescrip.SetFontSize(fontSize);
                    cellContenidoDescrip.SetBorder(Border.NO_BORDER);
                    cellContenidoDescrip.SetTextAlignment(TextAlignment.LEFT);
                    cellContenidoDescrip.SetFontColor(ColorConstants.RED);

                    Cell cellContenidoPrecio = new Cell();
                    cellContenidoPrecio.Add(new Paragraph("$ " + Convert.ToString(item.Precio)));
                    cellContenidoPrecio.SetBorder(Border.NO_BORDER);
                    cellContenidoPrecio.SetFontSize(fontSize);
                    cellContenidoPrecio.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    cellContenidoPrecio.SetTextAlignment(TextAlignment.CENTER);
                    cellContenidoPrecio.SetFontColor(ColorConstants.RED);

                    Cell cellContenidoCant = new Cell();
                    cellContenidoCant.Add(new Paragraph(Convert.ToString(item.Stock + "  -Poco Stock")));
                    cellContenidoCant.SetFontSize(fontSize);
                    cellContenidoCant.SetBorder(Border.NO_BORDER);
                    cellContenidoCant.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    cellContenidoCant.SetTextAlignment(TextAlignment.CENTER);
                    cellContenidoCant.SetFontColor(ColorConstants.RED);


                    maestro.AddCell(cellContenidoDescrip);
                    maestro.AddCell(cellContenidoPrecio);
                    maestro.AddCell(cellContenidoCant);
                }
                else
                {
                    Cell cellContenidoDescrip = new Cell();
                    cellContenidoDescrip.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    cellContenidoDescrip.Add(new Paragraph(item.Descripcion));
                    cellContenidoDescrip.SetFontSize(fontSize);
                    cellContenidoDescrip.SetBorder(Border.NO_BORDER);
                    cellContenidoDescrip.SetTextAlignment(TextAlignment.LEFT);
                    cellContenidoDescrip.SetFontColor(ColorConstants.DARK_GRAY);

                    Cell cellContenidoPrecio = new Cell();
                    cellContenidoPrecio.Add(new Paragraph("$ " + Convert.ToString(item.Precio)));
                    cellContenidoPrecio.SetBorder(Border.NO_BORDER);
                    cellContenidoPrecio.SetFontSize(fontSize);
                    cellContenidoPrecio.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    cellContenidoPrecio.SetTextAlignment(TextAlignment.CENTER);
                    cellContenidoPrecio.SetFontColor(ColorConstants.DARK_GRAY);

                    Cell cellContenidoCant = new Cell();
                    cellContenidoCant.Add(new Paragraph(Convert.ToString(item.Stock)));
                    cellContenidoCant.SetBorder(Border.NO_BORDER);
                    cellContenidoCant.SetFontSize(fontSize);
                    cellContenidoCant.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                    cellContenidoCant.SetTextAlignment(TextAlignment.CENTER);
                    cellContenidoCant.SetFontColor(ColorConstants.DARK_GRAY);

                    maestro.AddCell(cellContenidoDescrip);
                    maestro.AddCell(cellContenidoPrecio);
                    maestro.AddCell(cellContenidoCant);
                }
            }
            return maestro;
        }
    }
}
