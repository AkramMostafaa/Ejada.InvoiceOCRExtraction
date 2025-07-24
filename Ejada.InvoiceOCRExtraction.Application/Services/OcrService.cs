using Ejada.InvoiceOCRExtraction.Application.Dtos;
using Ejada.InvoiceOCRExtraction.Application.Helpers;
using Ejada.InvoiceOCRExtraction.Application.IServices;
using Ejada.InvoiceOCRExtraction.Application.Shared;
using Microsoft.AspNetCore.Http;
using Tesseract;


namespace Ejada.InvoiceOCRExtraction.Application.Services;

public class OcrService : IOcrService
{
    /// <summary>
    /// Upload an invoice file (image/pdf) and extract data using OCR
    /// </summary>
    /// <param name="file">The invoice file to upload.</param>
    public async Task<GeneralResponse<InvoiceDto>> ExtractInvoiceData(IFormFile fileToExtract)
    {

        var fileExtension = Path.GetExtension(fileToExtract.FileName);
        var fileStream = fileToExtract.OpenReadStream();

        if (!IsSupportedFileType(fileExtension))
            throw new NotSupportedException($"File type {fileExtension} is not supported");

        string tempFilePath = Path.GetTempFileName() + fileExtension;
        string imagePath = string.Empty;

        try
        {
            await using (var file = File.Create(tempFilePath))
            {
                await fileStream.CopyToAsync(file);
            }

            imagePath = fileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase)
                ? PdfConverterHelper.ConvertPdfToImage(tempFilePath) : tempFilePath;

            var tessDataPath = Path.Combine(AppContext.BaseDirectory, "TesseractData");
            using var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath);
            using var page = engine.Process(img);

            var extractedText = page.GetText();

            var parsedText = InvoiceParserHelper.ParseText(extractedText);
            return GeneralResponse<InvoiceDto>.SuccessResponse(parsedText, "Invoice parsed successfully.");
        }
        catch (Exception ex)
        {
            return GeneralResponse<InvoiceDto>.FailResponse("OCR parsing failed.", System.Net.HttpStatusCode.BadRequest);
        }
        finally
        {
            Cleanup(tempFilePath);
            if (!string.IsNullOrWhiteSpace(imagePath) && imagePath != tempFilePath)
                Cleanup(imagePath);

        }
    }

    // Clean Up Recourses
    private void Cleanup(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>Determines whether [is supported file type] [the specified extension].</summary>
    /// <param name="extension">The extension.</param>
    /// <returns>
    ///   <c>true</c> if [is supported file type] [the specified extension]; otherwise, <c>false</c>.</returns>
    private bool IsSupportedFileType(string extension) =>
       extension.ToLower() switch
       {
           ".pdf" or ".jpg" or ".jpeg" or ".png" => true,
           _ => false
       };
}