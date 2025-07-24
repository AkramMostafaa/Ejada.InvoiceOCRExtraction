namespace Ejada.InvoiceOCRExtraction.Application.Dtos;

public class InvoiceDetailDto
{
    public InvoiceDetailDto()
    {
        Description = string.Empty;
    }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal LineTotal { get; set; }
}
