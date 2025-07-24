using Ejada.InvoiceOCRExtraction.Application.Dtos;
using Ejada.InvoiceOCRExtraction.Application.Helpers;
using Ejada.InvoiceOCRExtraction.Application.IServices;
using Ejada.InvoiceOCRExtraction.Application.Shared;
using Ejada.InvoiceOCRExtraction.Domain.Entities;
using Ejada.InvoiceOCRExtraction.Domain.IRepositories;

namespace Ejada.InvoiceOCRExtraction.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoiceService(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    /// <summary>
    /// Save the extracted invoice data after user review
    /// </summary>
    /// <param name="dto">The invoice data to save.</param> 
    public async Task<GeneralResponse<Invoice>> CreateInvoiceAsync(InvoiceDto dto)
    {
        var invoice = InvoiceMapper.MapToEntity(dto);
        await _invoiceRepository.AddAsync(invoice);
        return GeneralResponse<Invoice>.SuccessResponse(invoice, "Invoice saved successfully.");
    }

}