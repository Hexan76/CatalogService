using FluentValidation;

namespace CatalogService.BusinessTypes;

public class CreateBusinessTypeModelValidation : AbstractValidator<CreateBusinessTypeRequest>
{
    public CreateBusinessTypeModelValidation()
    {
        RuleFor(x => x.Name)
            .NotEqual("string")
            .WithMessage("نباید string باشد");

        RuleFor(x => x.Description)
            .NotEqual("string")
            .WithMessage("نباید string باشد");
    }
}
