using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class UserValidator : AbstractValidator<UserViewModel>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .Length(3, 120).WithMessage("O nome deve ter entre 3 e 120 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado é inválido.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("O gênero informado é inválido.");

        RuleFor(x => x.AddressId)
            .NotEmpty().WithMessage("O endereço é obrigatório.");

        RuleFor(x => x.Avatar)
            .MaximumLength(500).WithMessage("A URL do avatar não pode ultrapassar 500 caracteres.")
            .Matches(@"^(https?:\/\/)?([\w-]+\.)+[\w-]+(\/[\w-]*)*\/?$")
            .When(x => !string.IsNullOrWhiteSpace(x.Avatar))
            .WithMessage("A URL do avatar deve ser válida.");
    }
}