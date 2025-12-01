using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class SessionScheduleValidator : AbstractValidator<SessionScheduleViewModel>
{
    public SessionScheduleValidator()
    {
        RuleFor(x => x.AvaliableAt)
            .GreaterThan(DateTime.UtcNow.AddMinutes(-5))
            .WithMessage("A data e hora da sessão devem ser no futuro.");

        RuleFor(x => x.DurationMinute)
            .Must(d => d.TotalMinutes >= 15 && d.TotalMinutes <= 240)
            .WithMessage("A duração da sessão deve estar entre 15 e 240 minutos.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("O status informado é inválido.");
    }
}