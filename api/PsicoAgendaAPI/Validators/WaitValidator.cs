using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class WaitValidator : AbstractValidator<WaitViewModel>
{
    public WaitValidator()
    {
        RuleFor(x => x.PreferrdeTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("O horário preferido deve ser uma data futura.");

        RuleFor(x => x.CreatedaAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data de criação não pode estar no futuro.");

        RuleFor(x => x.UpdatedAt)
            .GreaterThanOrEqualTo(x => x.CreatedaAt)
            .WithMessage("A data de atualização não pode ser anterior à data de criação.");
    }
}