using Ejada.InvoiceOCRExtraction.Application.Dtos;
using System.Text.RegularExpressions;

namespace Ejada.InvoiceOCRExtraction.Application.Helpers;

public static class InvoiceParserHelper
{
    /// <summary>
    /// Parses a raw text input representing an invoice and extracts relevant data into an <see cref="InvoiceDto"/>.
    /// The input text is expected to contain lines formatted with specific labels. Each label is compared 
    /// against predefined constants from the <see cref="InvoiceLabels"/> class. If a line matches a known label, 
    /// </summary>
    /// <param name="text">The raw text input from which invoice data will be extracted.</param>
    /// <returns>An instance of <see cref="InvoiceDto"/> populated with the extracted data.</returns>
 
    public static InvoiceDto ParseText(string text)
    {
        var dto = new InvoiceDto { InvoiceDate = DateTime.UtcNow };

        var lines = NormalizeLines(text.Split('\n', StringSplitOptions.RemoveEmptyEntries));

        foreach (var line in lines)
        {
            var trimmed = line.Trim();
            var value = ExtractValue(trimmed);

            if (TryExtractLabeledValue(trimmed, InvoiceLabels.InvoiceNumber, v => dto.InvoiceNumber = v)) 
                continue;

            if (TryExtractLabeledValue(trimmed, InvoiceLabels.Date, v =>
            {
                if (DateTime.TryParse(v, out var date)) dto.InvoiceDate = date;
            })) continue;

            if (TryExtractLabeledValue(trimmed, InvoiceLabels.Customer, v => dto.CustomerName = v)) 
                continue;

            if (TryExtractLabeledValue(trimmed, InvoiceLabels.TotalAmount, v =>
            {
                if (decimal.TryParse(v, out var total)) dto.TotalAmount = total;
            })) continue;

            if (TryExtractLabeledValue(trimmed, InvoiceLabels.VAT, v =>
            {
                if (decimal.TryParse(v, out var vat)) dto.VAT = vat;
            })) continue;

            var item = ParseLineItem(trimmed);

            if (item != null)
                dto.InvoiceDetails.Add(item);
        }

        return dto;
    }

    private static bool TryExtractLabeledValue(string line, string label, Action<string> assign)
    {
        if (line.StartsWith(label, StringComparison.OrdinalIgnoreCase))
        {
            var value = ExtractValue(line);
            assign(value);
            return true;
        }
        return false;
    }



    /// <summary>
    /// Normalizes an array of raw lines by trimming whitespace and merging lines where necessary.
    /// This preprocessing step helps ensure that the invoice data is structured correctly before 
    /// further extraction.
    /// </summary>
    /// <param name="rawLines">An array of raw string lines representing the invoice text.</param>
    /// <returns>A list of normalized lines, each prepared for parsing.</returns>
    private static List<string> NormalizeLines(string[] rawLines)
    {
        return rawLines
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();
    }
    /// <summary>
    /// Parses a single line item from the invoice, extracting the quantity, description, and total amount.
    /// </summary>
    /// <param name="line">A string representation of a line item from the invoice.</param>
    /// <returns>An instance of <see cref="InvoiceDetailDto"/> populated with the extracted line item data, 
    /// or null if the line does not match the expected format.</returns>
    private static InvoiceDetailDto? ParseLineItem(string line)
    {
        var pattern = new Regex(@"^(?<qty>\d+)\s*(?<desc>[A-Za-z\- ]+?)\s+(?<total>\d+(?:\.\d{1,2})?)$", RegexOptions.Compiled);
        var match = pattern.Match(line);

        if (!match.Success)
            return null;

        var quantity = int.Parse(match.Groups["qty"].Value);
        var description = match.Groups["desc"].Value.Trim();
        var lineTotal = decimal.Parse(match.Groups["total"].Value);
        var unitPrice = lineTotal / quantity;

        return new InvoiceDetailDto
        {
            Quantity = quantity,
            Description = description,
            LineTotal = lineTotal,
            UnitPrice = Math.Round(unitPrice, 2)
        };
    }
    /// <summary>
    /// This method splits the input line at the first colon and returns the trimmed part 
    /// after the colon. If no colon is present, an empty string is returned.
    /// </summary>
    /// <param name="line">The line from which to extract the value.</param>
    /// <returns>The extracted value as a string, or an empty string if no value is found.</returns>
    private static string ExtractValue(string line)
    {
        var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 1 ? parts[1].Trim() : string.Empty;
    }

}
