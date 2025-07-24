using Ejada.InvoiceOCRExtraction.Application.Dtos;
using Ejada.InvoiceOCRExtraction.Application.Shared;
using Ejada.InvoiceOCRExtraction.Domain.Entities;

namespace Ejada.InvoiceOCRExtraction.Application.IServices;

public interface IInvoiceService
{
    Task<GeneralResponse<Invoice>> CreateInvoiceAsync(InvoiceDto invoice);
}