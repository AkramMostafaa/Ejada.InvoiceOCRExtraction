namespace Ejada.InvoiceOCRExtraction.Application.Dtos;

public class InvoiceDto
{
    public InvoiceDto()
    {
        CustomerName = string.Empty;
        InvoiceNumber = string.Empty;
    }
    public string InvoiceNumber { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal VAT { get; set; }
    public List<InvoiceDetailDto> InvoiceDetails { get; set; } = new();
}