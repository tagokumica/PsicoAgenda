using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class PatientValidator : AbstractValidator<PatientViewModel>
{
    public PatientValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do paciente é obrigatório.")
            .Length(3, 120).WithMessage("O nome deve ter entre 3 e 120 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O e-mail é obrigatório.")
            .EmailAddress().WithMessage("O e-mail informado é inválido.");

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("A data de nascimento é obrigatória.")
            .LessThan(DateTime.UtcNow).WithMessage("A data de nascimento não pode ser futura.")
            .GreaterThan(DateTime.UtcNow.AddYears(-120)).WithMessage("A idade não pode ser superior a 120 anos.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Length(11).WithMessage("O CPF deve ter 11 caracteres.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("As observações não podem ultrapassar 500 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Notes));

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("O gênero informado é inválido.");

        RuleFor(x => x.EmergencyContract)
            .NotEmpty().WithMessage("O contato de emergência é obrigatório.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("O contato de emergência deve conter entre 10 e 15 dígitos numéricos (com DDD).");
    }
}
