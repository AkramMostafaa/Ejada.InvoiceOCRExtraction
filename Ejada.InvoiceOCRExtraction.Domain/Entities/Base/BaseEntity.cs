namespace Ejada.InvoiceOCRExtraction.Domain.Entities.Base;

public class BaseEntity
{
    public BaseEntity()
    {
        CreatedAt  = DateTime.UtcNow;
        UpdatedAt  = DateTime.UtcNow;
    }               
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
