using CfpService.Contracts.Dtos.Application;
using FluentValidation;

namespace CfpService.Application.Validators;

public class PutApplicationDtoValidator : AbstractValidator<PutApplicationDto>
{
    private const int MaxNameLength = 100;
    private const int MaxDescriptionLength = 300;
    private const int MaxOutlineLength = 1000;
    public PutApplicationDtoValidator()
    {
        var validActivities = new[] { "Report", "Masterclass", "Discussion" };
        
        RuleFor(x => x)
            .Must(x => !string.IsNullOrWhiteSpace(x.Activity) ||
                                      !string.IsNullOrWhiteSpace(x.Name) ||
                                      !string.IsNullOrWhiteSpace(x.Description) ||
                                      !string.IsNullOrWhiteSpace(x.Outline))
            .WithMessage("at least one other field besides author");

        RuleFor(x => x.Name).MaximumLength(MaxNameLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage($"name must not exceed {MaxNameLength} chars");

        RuleFor(x => x.Description).MaximumLength(MaxDescriptionLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage($"description must not exceed {MaxDescriptionLength} chars");

        RuleFor(x => x.Outline).MaximumLength(MaxOutlineLength)
            .When(x => !string.IsNullOrWhiteSpace(x.Outline))
            .WithMessage($"outline must not exceed {MaxOutlineLength} chars");
        
        RuleFor(x => x.Activity).Must(activity => validActivities.Contains(activity))
            .When(x => !string.IsNullOrEmpty(x.Activity))
            .WithMessage($"Activity must be one of the following values: {string.Join(", ", validActivities)}");
    }
}