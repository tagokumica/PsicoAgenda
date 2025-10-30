using FluentValidation.TestHelper;
using PsicoAgendaAPI.Validators;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaTests.Api.Validators;

public class AddressValidatorTests
{
    private readonly AddressValidator _validator = new();

    [Fact(DisplayName = "Deve falhar se rua estiver vazia ou curta")]
    public void Should_Fail_When_StreetInvalid()
    {
        var model = new AddressViewModel (Guid.NewGuid(), "", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Street);
    }

    [Fact(DisplayName = "Deve passar se rua for válida")]
    public void Should_Pass_When_StreetValid()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua Flores", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.Street);
    }

    [Fact(DisplayName = "Deve falhar se número for menor ou igual a zero")]
    public void Should_Fail_When_NumberInvalid()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua A", 0, "casa 2", "São José do Rio Preto", "15050-230",
            "SP");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Number);
    }

    [Fact(DisplayName = "Deve falhar se CEP for inválido")]
    public void Should_Fail_When_ZipCodeInvalid()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "1234567",
            "SP");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ZipCode);
    }

    [Fact(DisplayName = "Deve passar se CEP for válido")]
    public void Should_Pass_When_ZipCodeValid()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230",
            "SP");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.ZipCode);
    }

    [Fact(DisplayName = "Deve falhar se estado tiver tamanho incorreto")]
    public void Should_Fail_When_StateInvalid()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230",
            "São Paulo");
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.State);
    }

    [Fact(DisplayName = "Deve passar se estado tiver 2 caracteres")]
    public void Should_Pass_When_StateValid()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230",
            "SP");
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.State);
    }

    [Fact(DisplayName = "Deve validar comprimento máximo do complemento")]
    public void Should_Validate_Complement_Length()
    {
        var model = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, new string('A', 101), "São José do Rio Preto", "15050-230",
            "SP");

        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.Complement);
    }
}