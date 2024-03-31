using CfpService.Dtos.Application;
using FluentValidation;


namespace CfpService.Validators;

public class PostApplicationDtoValidator : AbstractValidator<PostApplicationDto>
{
    public PostApplicationDtoValidator()
    {
        RuleFor(x => x.Author).NotEmpty().WithMessage("Идентификатор пользователя обязателен.");

        // Правило для проверки, что хотя бы одно поле, кроме Author, не пустое
        RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Activity) ||
                                  !string.IsNullOrWhiteSpace(x.Name) ||
                                  !string.IsNullOrWhiteSpace(x.Description) ||
                                  !string.IsNullOrWhiteSpace(x.Outline))
            .WithMessage("Необходимо указать хотя бы одно дополнительное поле.");

        RuleFor(x => x.Name).MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Name))
            .WithMessage("Название не должно превышать 100 символов.");

        RuleFor(x => x.Description).MaximumLength(300).When(x => !string.IsNullOrWhiteSpace(x.Description))
            .WithMessage("Описание не должно превышать 300 символов.");

        RuleFor(x => x.Outline).MaximumLength(1000).When(x => !string.IsNullOrWhiteSpace(x.Outline))
            .WithMessage("План не должен превышать 1000 символов.");
    }
}