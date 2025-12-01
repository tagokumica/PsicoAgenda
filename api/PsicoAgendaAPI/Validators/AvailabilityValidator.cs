using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class AvailabilityValidator : AbstractValidator<AvailabilitieViewModel>
{
    public AvailabilityValidator()
    {
        RuleFor(x => x.AvaliableAt)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("A data de disponibilidade deve ser no futuro.");

        RuleFor(x => x.DurationMinutes)
            .Must(duration => duration.TotalMinutes >= 15 && duration.TotalMinutes <= 240)
            .WithMessage("A duração deve ser entre 15 e 240 minutos.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty()
            .WithMessage("O campo 'CreatedBy' é obrigatório.");

        RuleFor(x => x.Source)
            .IsInEnum()
            .WithMessage("O campo 'Source' contém um valor inválido.");

        RuleFor(x => x.TypeAvailabilitie)
            .IsInEnum()
            .WithMessage("O campo 'TypeAvailabilitie' contém um valor inválido.");

        RuleFor(x => x.Location)
            .MaximumLength(200)
            .WithMessage("A localização não pode ter mais de 200 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Location));

        RuleFor(x => x.MeetUrl)
            .Matches(@"^(https?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w-]*)*\/?$")
            .WithMessage("A URL da reunião deve ser válida.")
            .When(x => !string.IsNullOrWhiteSpace(x.MeetUrl));
    }
}