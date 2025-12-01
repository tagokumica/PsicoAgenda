using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class TermsOfUseValidator : AbstractValidator<TermsOfUseViewModel>
{
    public TermsOfUseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do termo é obrigatório.")
            .Length(3, 200).WithMessage("O nome deve ter entre 3 e 200 caracteres.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("O conteúdo do termo é obrigatório.")
            .MaximumLength(10000).WithMessage("O conteúdo não pode ultrapassar 10.000 caracteres.");

        RuleFor(x => x.CreatedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data de criação não pode estar no futuro.");

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("A versão do termo é obrigatória.")
            .Matches(@"^\d+(\.\d+)*$")
            .WithMessage("A versão deve estar no formato numérico, por exemplo: 1.0 ou 2.1.3.");
    }
}
