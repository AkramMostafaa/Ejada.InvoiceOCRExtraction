using Ejada.InvoiceOCRExtraction.Application.Dtos;
using Ejada.InvoiceOCRExtraction.Domain.Entities;

namespace Ejada.InvoiceOCRExtraction.Application.Helpers;

public static class InvoiceMapper
{

  /// <summary>Maps the InvoiceDto to entity.</summary>
  /// <param name="dto">The dto.</param>
  /// <returns>
  /// Invoice
  ///   <br />
  /// </returns>
  public static Invoice MapToEntity(InvoiceDto dto)
  {
    DateTime utcTime = dto.InvoiceDate.ToUniversalTime();
    var invoice = new Invoice(
            dto.InvoiceNumber ?? string.Empty,
            utcTime,
            dto.CustomerName ?? string.Empty,
            dto.TotalAmount,
            dto.VAT
        );

    foreach (var item in dto.InvoiceDetails)
    {
      var detail = new InvoiceDetail(
          item.Description ?? string.Empty,
          item.Quantity,
          item.UnitPrice,
          item.LineTotal
      );
      invoice.AddInvoiceDetails(detail);
    }

    return invoice;
  }
}
