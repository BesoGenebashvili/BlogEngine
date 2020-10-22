using System.Threading.Tasks;
using BlogEngine.Core.Helpers;
using BlogEngine.Server.Services.Abstractions;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.IO;

namespace BlogEngine.Server.Services.Implementations
{
    public class PDFGenerator : IPDFGenerator
    {
        public async Task<byte[]> GeneratePDFAsync(string htmlContent)
        {
            if (string.IsNullOrWhiteSpace(htmlContent))
            {
                Throw.ArgumentNullException(nameof(htmlContent));
            }

            return await Task.Run(() => ProcessGeneratePDF(htmlContent));
        }

        public byte[] ProcessGeneratePDF(string htmlContent)
        {
            byte[] result = null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                var pdf = PdfGenerator.GeneratePdf(htmlContent, PdfSharp.PageSize.A4);
                pdf.Save(memoryStream);
                result = memoryStream.ToArray();
            }

            return result;
        }
    }
}