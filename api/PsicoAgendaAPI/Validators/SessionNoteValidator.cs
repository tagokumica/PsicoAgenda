using FluentValidation;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Validators;

public class SessionNoteValidator : AbstractValidator<SessionNoteViewModel>
{
    public SessionNoteValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("O conteúdo da anotação é obrigatório.")
            .Length(10, 2000).WithMessage("O conteúdo deve ter entre 10 e 2000 caracteres.");

        RuleFor(x => x.Tags)
            .NotNull().WithMessage("As tags não podem ser nulas.")
            .Must(tags => tags.Length <= 10).WithMessage("Não é permitido mais de 10 tags por anotação.")
            .ForEach(tag =>
            {
                tag.NotEmpty().WithMessage("As tags não podem estar vazias.")
                    .MaximumLength(30).WithMessage("Cada tag deve ter no máximo 30 caracteres.");
            });

        RuleFor(x => x.Insight)
            .MaximumLength(1000).WithMessage("O insight não pode ultrapassar 1000 caracteres.")
            .When(x => !string.IsNullOrWhiteSpace(x.Insight));
    }
}