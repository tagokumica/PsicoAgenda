using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class LocationValidator : AbstractValidator<LocationViewModel>
{
    public LocationValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do local é obrigatório.")
            .Length(3, 100).WithMessage("O nome do local deve ter entre 3 e 100 caracteres.");
    }
}