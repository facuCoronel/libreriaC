using Bbl.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bbl.Implementacion
{
    public class ImplementacionAdjuntarPDF : IServicioAdjuntarPDF
    {

        public async Task<byte[]> AdjuntarPDF(List<byte[]> pdfArray)
        {
            if(pdfArray.Count != 0)
            {
                using (var ms1 = new MemoryStream())
                {
                    var pdf1 = new PdfDocument(new PdfWriter(ms1));
                    var merger = new PdfMerger(pdf1);
                    foreach (var byteArray in pdfArray)
                    {
                        using (var ms = new MemoryStream(byteArray))
                        {
                            var pdf2 = new PdfDocument(new PdfReader(ms));
                            merger.Merge(pdf2, 1, pdf2.GetNumberOfPages());
                            pdf2.Close();
                        }
                    }
                    pdf1.Close();
                    var result = ms1.ToArray();
                    ms1.Close();
                    return result;
                }
            }
            else
            {
                throw new Exception("La lista fue null");
            }
        }




    }
}
