using Ejada.InvoiceOCRExtraction.Application.Dtos;
using FluentValidation;

namespace Ejada.InvoiceOCRExtraction.Application.Validators;

public class InvoiceDetailDtoValidator : AbstractValidator<InvoiceDetailDto>
{
    public InvoiceDetailDtoValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Item description is required.")
            .MaximumLength(255);

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Unit price must be non-negative.");

        RuleFor(x => x.LineTotal)
            .GreaterThanOrEqualTo(0).WithMessage("Line total must be non-negative.");
    }
}