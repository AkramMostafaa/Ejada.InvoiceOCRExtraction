using Ejada.InvoiceOCRExtraction.Application.Dtos;
using Ejada.InvoiceOCRExtraction.Application.Shared;
using Microsoft.AspNetCore.Http;

namespace Ejada.InvoiceOCRExtraction.Application.IServices;

public interface IOcrService
{
    Task<GeneralResponse<InvoiceDto>> ExtractInvoiceData(IFormFile fileToExtract);
}

