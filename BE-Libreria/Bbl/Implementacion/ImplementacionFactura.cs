using Modelos;
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
using Modelos;
using Bbl.Interfaces;

namespace Bbl.Implementacion
{
    public class ImplementacionFactura : IServicioFacturaPDF
    {

        public async Task<byte[]> GenerarFactura(List<FacturaPDF> lFactura, string fecha)
        {
            using (MemoryStream ms = new MemoryStream())
            using (PdfWriter writer = new PdfWriter(ms))
            using (PdfDocument pdfDoc = new PdfDocument(writer))
            using (Document document = new Document(pdfDoc))
            {
                pdfDoc.SetDefaultPageSize(PageSize.A4);

                //Logo

                document.Add(new ImplementacionLogo().CrearLogo());

                //cabecera

                Text titulo = new Text("Factura").SetFontColor(ColorConstants.DARK_GRAY);
                Paragraph header = new Paragraph(titulo);
                header.SetTextAlignment(TextAlignment.LEFT);
                header.SetFontSize(20);
                header.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));
                document.Add(header);

                //Datos
                string? cli;
                foreach(var item in lFactura)
                {
                    if(item != null)
                    {
                        cli = item.Cliente;
                        Paragraph cliente = new Paragraph($"Cliente : {cli}").SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetFont(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA));
                        document.Add(cliente);
                        break;
                    }
                }
               
                Paragraph dateInit = new Paragraph("Fecha : " + fecha).SetTextAlignment(TextAlignment.LEFT).SetFontSize(12).SetFont(PdfFontFactory.CreateFont(StandardFontFamilies.HELVETICA));
                document.Add(dateInit);



                //Tabla de productos

                document.Add((await new ImplementacionTablaFactura().CrearTabla(lFactura)));


                document.Add((await new ImplementacionTablaFactura().CrearTablaResumen(lFactura)));

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
