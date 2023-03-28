using Bbl.Interfaces;
using Modelos;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout.Borders;
using iText.Layout.Renderer;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Events;
using iText.IO.Image;

namespace Bbl.Implementacion
{
    public class ImplementacionListarProductos : IServicioListaProductosPDF
    {
        public async Task<byte[]> GenerarListaProducto(List<ListaProductoPDF> lOrden, string fechaDesde, string fechaHasta)
        {
            //string path = "./ListaProducto.pdf";
            using (MemoryStream ms = new MemoryStream())
            using (PdfWriter writer = new PdfWriter(ms))
            using (PdfDocument pdfDoc = new PdfDocument(writer))
            using (Document document = new Document(pdfDoc))
            {
                pdfDoc.SetDefaultPageSize(PageSize.A4);

                //Logo

                document.Add(new ImplementacionLogo().CrearLogo());

                //cabecera

                Text titulo = new Text("Lista de Productos").SetFontColor(ColorConstants.DARK_GRAY);
                Paragraph header = new Paragraph(titulo);
                header.SetTextAlignment(TextAlignment.LEFT);
                header.SetFontSize(20);
                header.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                document.Add(header);

                //fecha
                Paragraph dateInit = new Paragraph("Fecha desde : " + fechaDesde).SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetFont(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA));
                document.Add(dateInit);
                Paragraph dateEnd = new Paragraph("Fecha hasta : " + fechaHasta).SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetFont(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA));
                document.Add(dateEnd);


                //Tabla de productos

                document.Add(await new ImplementacionTablaProductoStock().CrearTabla(lOrden));

                //--------Fin tabla productos-------



                //footer

                pdfDoc.AddEventHandler(PdfDocumentEvent.END_PAGE, new ImplementacionFooter(document));

                document.Close();

                //retorno en byte el pdf.
                var respuesta = ms.ToArray();

                return respuesta;

            }
        }
    }
}

