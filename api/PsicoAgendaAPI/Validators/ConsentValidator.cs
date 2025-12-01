using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class ConsentValidator : AbstractValidator<ConsentViewModel>
{
    public ConsentValidator()
    {
        RuleFor(x => x.ConsentType)
            .NotEmpty().WithMessage("O tipo de consentimento é obrigatório.")
            .Length(3, 100).WithMessage("O tipo de consentimento deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.GivenAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A data de consentimento não pode estar no futuro.");

        RuleFor(x => x.Version)
            .NotEmpty().WithMessage("A versão do termo é obrigatória.")
            .Matches(@"^\d+(\.\d+)*$")
            .WithMessage("A versão deve estar no formato numérico, por exemplo: 1.0, 2.3.1.");
    }
}