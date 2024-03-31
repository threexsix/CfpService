using CfpService.Dtos.Application;
using FluentValidation;


namespace CfpService.Validators;

public class PostApplicationDtoValidator : AbstractValidator<PostApplicationDto>
{
    public PostApplicationDtoValidator()
    {
        RuleFor(x => x.Author).NotEmpty().WithMessage("missing author");

        RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Activity) ||
                                  !string.IsNullOrWhiteSpace(x.Name) ||
                                  !string.IsNullOrWhiteSpace(x.Description) ||
                                  !string.IsNullOrWhiteSpace(x.Outline))
            .WithMessage("at least one other field besides author");

        RuleFor(x => x.Name).MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("name must not exceed 100 chars");

        RuleFor(x => x.Description).MaximumLength(300).When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("description must not exceed 300 chars");

        RuleFor(x => x.Outline).MaximumLength(1000).When(x => !string.IsNullOrWhiteSpace(x.Outline))
            .WithMessage("outline must not exceed 1000 chars");
    }
}