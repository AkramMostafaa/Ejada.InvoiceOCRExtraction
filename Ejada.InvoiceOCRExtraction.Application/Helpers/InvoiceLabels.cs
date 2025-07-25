namespace Ejada.InvoiceOCRExtraction.Application.Helpers;

public static class InvoiceLabels
{
  public static string[] InvoiceNumber => new[] { "Invoice Number", "No", "Inv #" };
  public static string[] Date => new[] { "Date" };
  public static string[] Customer => new[] { "Customer Name", "Customer" };
  public static string[] TotalAmount => new[] { "Total", "Total Amount", "Grand Total" };
  public static string[] VAT => new[] { "VAT" };
}
