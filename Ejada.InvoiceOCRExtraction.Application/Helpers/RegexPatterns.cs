using System.Text.RegularExpressions;

namespace Ejada.InvoiceOCRExtraction.Application.Helpers;

/// <summary>
/// Helper Class To Store The Regex Pattern For Different Types Of Invoice Data To Avoid Write Regex strings in the Main Method 
///   <br />
/// </summary>
public static class RegexPatterns
{
    public static Regex InvoiceNumber() => new(@"Invoice\s*Number[:\-]?\s*(\w+)", RegexOptions.IgnoreCase);
    public static Regex Date() => new(@"Date[:\-]?\s*(\d{1,2}[/-]\d{1,2}[/-]\d{2,4})", RegexOptions.IgnoreCase);
    public static Regex Customer() => new(@"Customer[:\-]?\s*(.+)", RegexOptions.IgnoreCase);
    public static Regex TotalAmount() => new(@"Total\s*Amount[:\-]?\s*([\d,]+\.\d{2})", RegexOptions.IgnoreCase);
    public static Regex VAT() => new(@"VAT[:\-]?\s*([\d,]+\.\d{2})", RegexOptions.IgnoreCase);
}
