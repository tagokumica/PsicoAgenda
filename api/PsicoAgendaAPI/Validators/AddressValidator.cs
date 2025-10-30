using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class AddressValidator : AbstractValidator<AddressViewModel>
{
    public AddressValidator()
    {
        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("A rua é obrigatória.")
            .Length(3, 100).WithMessage("A rua deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.Number)
            .GreaterThan(0).WithMessage("O número deve ser maior que zero.");

        RuleFor(x => x.Complement)
            .MaximumLength(100).WithMessage("O complemento não pode exceder 100 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Complement));

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("A cidade é obrigatória.")
            .Length(2, 60).WithMessage("A cidade deve ter entre 2 e 60 caracteres.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("O CEP é obrigatório.")
            .Matches(@"^\d{5}-?\d{3}$").WithMessage("CEP inválido. Use o formato 00000-000.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("O estado é obrigatório.")
            .Length(2).WithMessage("O estado deve conter exatamente 2 caracteres (UF).");
    }
}