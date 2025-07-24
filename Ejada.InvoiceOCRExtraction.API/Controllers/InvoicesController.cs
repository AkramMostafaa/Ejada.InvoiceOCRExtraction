using Ejada.InvoiceOCRExtraction.Application.Dtos;
using Ejada.InvoiceOCRExtraction.Application.IServices;
using Ejada.InvoiceOCRExtraction.Application.Shared;
using Ejada.InvoiceOCRExtraction.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ejada.InvoiceOCRExtraction.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly IOcrService _ocrService;
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IOcrService ocrService, IInvoiceService invoiceService)
    {
        _ocrService = ocrService;
        _invoiceService = invoiceService;
    }


    [HttpPost("upload")]
    [ProducesResponseType(typeof(GeneralResponse<InvoiceDto>), 200)]
    [ProducesResponseType(typeof(GeneralResponse<InvoiceDto>), 400)]
    [ProducesResponseType(typeof(GeneralResponse<InvoiceDto>), 500)]
    public async Task<GeneralResponse<InvoiceDto>> UploadInvoice(IFormFile file)
    {
        var response = await _ocrService.ExtractInvoiceData(file);
        return response;
    }

    [HttpPost("submit")]
    [ProducesResponseType(typeof(GeneralResponse<InvoiceDto>), 200)]
    [ProducesResponseType(typeof(GeneralResponse<InvoiceDto>),400)]
    [ProducesResponseType(typeof(GeneralResponse<InvoiceDto>), 500)]
    public async Task<GeneralResponse<Invoice>> SubmitInvoice([FromBody] InvoiceDto dto)
    {
        return  await _invoiceService.CreateInvoiceAsync(dto);
    }
}
