using PdfiumViewer;
using System.Drawing.Imaging;

namespace Ejada.InvoiceOCRExtraction.Application.Helpers;

public static class PdfConverterHelper
{
    /// <summary>Converts the PDF to image.</summary>
    /// <param name="pdfPath">The PDF path.</param>
    /// <returns>
    /// ImagePath
    ///   <br />
    /// </returns>
    public static string ConvertPdfToImage(string pdfPath)
    {
        using var document = PdfDocument.Load(pdfPath);
        var image = document.Render(0, 300, 300, true);
        var imagePath = Path.ChangeExtension(pdfPath, ".png");
        image?.Save(imagePath, ImageFormat.Png);
        return imagePath;
    }
}
