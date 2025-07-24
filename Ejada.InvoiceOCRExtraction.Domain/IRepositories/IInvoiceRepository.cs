using Ejada.InvoiceOCRExtraction.Domain.Entities;

namespace Ejada.InvoiceOCRExtraction.Domain.IRepositories;

public interface IInvoiceRepository
{
    Task<Invoice> AddAsync(Invoice invoice);
    Task<Invoice?> GetByIdAsync(int id);
}
