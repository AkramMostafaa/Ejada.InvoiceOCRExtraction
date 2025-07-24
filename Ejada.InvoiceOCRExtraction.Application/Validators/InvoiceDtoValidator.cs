using Ejada.InvoiceOCRExtraction.Application.Dtos;
using FluentValidation;

namespace Ejada.InvoiceOCRExtraction.Application.Validators;

public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
{
    public InvoiceDtoValidator()
    {
        RuleFor(x => x.InvoiceNumber)
            .NotEmpty().WithMessage("Invoice number is required.")
            .MaximumLength(50);

        RuleFor(x => x.InvoiceDate)
            .NotEmpty().WithMessage("Invoice date is required.")
            .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Invoice date cannot be in the future.");

        RuleFor(x => x.CustomerName)
            .NotEmpty().WithMessage("Customer name is required.")
            .MaximumLength(100);

        RuleFor(x => x.TotalAmount)
            .GreaterThanOrEqualTo(0).WithMessage("Total amount must be positive.");

        RuleFor(x => x.VAT)
            .GreaterThanOrEqualTo(0).WithMessage("VAT must be positive.");
        RuleForEach(x => x.InvoiceDetails)
         .SetValidator(new InvoiceDetailDtoValidator());

    }
}