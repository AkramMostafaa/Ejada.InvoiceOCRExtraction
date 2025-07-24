using Ejada.InvoiceOCRExtraction.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace Ejada.InvoiceOCRExtraction.Domain.Entities;

public class InvoiceDetail : BaseEntity
{
    public InvoiceDetail()
    {
        Description = string.Empty;
        Invoice = default!;
    }
    public InvoiceDetail(string description, int quantity, decimal unitPrice, decimal lineTotal)
    {
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
        LineTotal = lineTotal;
        Invoice = default!;
    }

    public string Description { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal LineTotal { get; private set; }
    public int InvoiceId { get; set; }
    [JsonIgnore]
    public Invoice Invoice { get; set; }
    public InvoiceDetail Update(string description, int quantity, decimal unitPrice, decimal lineTotal, int invoiceId, Invoice invoice)
    {
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
        LineTotal = lineTotal;
        InvoiceId = invoiceId;
        Invoice = invoice;
        return this;
    }
}
