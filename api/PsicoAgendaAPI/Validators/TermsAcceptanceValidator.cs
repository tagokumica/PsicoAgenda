using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class TermsAcceptanceValidator : AbstractValidator<TermsAcceptanceViewModel>
{
    public TermsAcceptanceValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("O conteúdo aceito é obrigatório.")
            .MaximumLength(1000).WithMessage("O conteúdo não pode ultrapassar 1.000 caracteres.");
        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data de aceitação não pode estar no futuro.");
    }
}