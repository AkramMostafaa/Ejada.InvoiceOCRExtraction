using Ejada.InvoiceOCRExtraction.Domain.Entities;
using Ejada.InvoiceOCRExtraction.Domain.IRepositories;
using Ejada.InvoiceOCRExtraction.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Ejada.InvoiceOCRExtraction.Infrastructure.Repositories;

public class InvoiceRepository : IInvoiceRepository
{
    private readonly InvoiceOCRDbContext _dbContext;

    public InvoiceRepository(InvoiceOCRDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Invoice> AddAsync(Invoice invoice)
    {
        await _dbContext.Invoices.AddAsync(invoice);
        await _dbContext.SaveChangesAsync();
        return invoice;
    }

    public async Task<Invoice?> GetByIdAsync(int id)
    {
        return await _dbContext.Invoices
            .Include(i => i.InvoiceDetails)
            .FirstOrDefaultAsync(i => i.Id == id);
    }


}
