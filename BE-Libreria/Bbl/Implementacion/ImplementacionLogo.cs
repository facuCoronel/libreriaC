using Bbl.Interfaces;
using iText.IO.Image;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    public class ImplementacionLogo : IServicioLogo
    {
        public Image CrearLogo()
        {
            Image logo = new Image(ImageDataFactory.Create("../Diseño/Assets/Logo.png"));
            logo.SetTextAlignment(TextAlignment.RIGHT);
            logo.SetHeight(250);
            logo.SetHorizontalAlignment(HorizontalAlignment.RIGHT);

            return logo;
        }
    }
}
