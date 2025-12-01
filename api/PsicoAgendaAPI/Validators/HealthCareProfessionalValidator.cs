using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class HealthCareProfessionalValidator : AbstractValidator<HealthCareProfissionalViewModel>
{
    public HealthCareProfessionalValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 120).WithMessage("O nome deve ter entre 3 e 120 caracteres.");

        RuleFor(x => x.CurriculumURL)
            .NotEmpty().WithMessage("A URL do currículo é obrigatória.")
            .Matches(@"^(https?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w-]*)*\/?$")
            .WithMessage("A URL do currículo deve ser válida.");

        RuleFor(x => x.UndergraduateURL)
            .NotEmpty().WithMessage("A URL da graduação é obrigatória.")
            .Matches(@"^(https?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w-]*)*\/?$")
            .WithMessage("A URL da graduação deve ser válida.");

        RuleFor(x => x.CrpOrCrmURL)
            .NotEmpty().WithMessage("A URL do registro profissional (CRP/CRM) é obrigatória.")
            .Matches(@"^(https?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w-]*)*\/?$")
            .WithMessage("A URL do registro profissional deve ser válida.");

        RuleFor(x => x.ApprovalStatus)
            .IsInEnum()
            .WithMessage("O status de aprovação é inválido.");

        RuleFor(x => x.AvailabilityStatus)
            .IsInEnum()
            .WithMessage("O status de disponibilidade é inválido.");
    }
}