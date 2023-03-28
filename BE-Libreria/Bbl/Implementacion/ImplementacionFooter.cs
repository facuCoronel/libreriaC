using Bbl.Interfaces;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    internal class ImplementacionFooter : IServicioFooter
    {
        protected Document doc;

        public ImplementacionFooter(Document doc)
        {
            this.doc = doc;
        }

        public void HandleEvent(Event currentEvent)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)currentEvent;
            Rectangle pageSize = docEvent.GetPage().GetPageSize();
            PdfFont font = null;
            try
            {
                font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            }
            catch (IOException e)
            {
                Console.Error.WriteLine(e.Message);
            }

            var tel = new Image(ImageDataFactory.Create("../Diseño/Assets/Logo.png"));
            tel.SetWidth(10);

            //Textos.
            var telText = new Text("(55) 1234-5678");

            var emailText = new Text("hola@sitioincreible.com");

            var sitioWebText = new Text("www.sitioincreible.com");
            //------------------------------------

            //Links. 
            var email = new Link(emailText.GetText(), PdfAction.CreateURI("https://www.google.com.ar"));
            var sitioWeb = new Link(sitioWebText.GetText(), PdfAction.CreateURI("https://www.google.com.ar"));
            //-----------------------------------
            Paragraph footer = new Paragraph($"{telText.GetText()}                        {email.GetText()}                        {sitioWeb.GetText()}");
            footer.SetTextAlignment(TextAlignment.CENTER);
            footer.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            footer.SetVerticalAlignment(VerticalAlignment.MIDDLE);
            footer.SetFontColor(ColorConstants.WHITE);
            footer.SetFontSize(10);
            footer.SetHeight(36);
            footer.SetWidth(uint.MaxValue);
            footer.SetBackgroundColor(ColorConstants.BLACK);

            float coordX = ((pageSize.GetLeft() + doc.GetLeftMargin())
                             + (pageSize.GetRight() - doc.GetRightMargin())) / 2;
            float footerY = doc.GetBottomMargin();
            Canvas canvas = new Canvas(docEvent.GetPage(), pageSize);
            canvas
                .SetFont(font)
                .SetFontSize(5)
                .ShowTextAligned(footer, coordX, footerY - 36, TextAlignment.CENTER)
                .Close();
        }
    }
}
