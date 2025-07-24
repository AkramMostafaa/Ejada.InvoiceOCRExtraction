using Ejada.InvoiceOCRExtraction.Domain.Entities.Base;

namespace Ejada.InvoiceOCRExtraction.Domain.Entities;

public class Invoice : BaseEntity
{
    public Invoice(string invoiceNumber, DateTime invoiceDate, string customerName, decimal totalAmount, decimal vat)
    {
        InvoiceNumber = invoiceNumber;
        InvoiceDate = invoiceDate;
        CustomerName = customerName;
        TotalAmount = totalAmount;
        VAT = vat;
        InvoiceDetails = new List<InvoiceDetail>();
    }
    public Invoice()
    {
        CustomerName = string.Empty;
        InvoiceNumber = string.Empty;
    }
    public string InvoiceNumber { get; private set; }
    public DateTime InvoiceDate { get; private set; }
    public string CustomerName { get; private set; }
    public decimal TotalAmount { get; private set; }
    public decimal VAT { get; private set; }
    public ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();


    public void AddInvoiceDetails(InvoiceDetail item)
    {
        InvoiceDetails.Add(item);
    }

    public Invoice Update(string invoiceNumber, DateTime invoiceDate, string customerName, decimal totalAmount, decimal vat)
    {
        InvoiceNumber = invoiceNumber;
        InvoiceDate = invoiceDate;
        CustomerName = customerName;
        TotalAmount = totalAmount;
        VAT = vat;
        return this;
    }
}