
using Domain.Validate;

namespace PsicoAgendaTests.Domain.Validate
{
    public class CpfValidatorTests
    {
        public static class CpfValidatorTestsData
        {
            public static readonly string[] ValidCpfs = new[]
            {
                "529.982.247-25",
                "52998224725",
                "168.995.350-09",
                "16899535009",
                "111.444.777-35",
                " 529.982.247-25 " // com espaços
            };
        }
        [Theory(DisplayName = "IsValid deve retornar TRUE para CPFs válidos")]
        [MemberData(nameof(GetValidCpfs))]
        public void IsValid_ReturnsTrue_ForValidCpfs(string cpf)
        {
            // Act
            var result = Document.IsValid(cpf);

            // Assert
            Assert.True(result);
        }

        public static System.Collections.Generic.IEnumerable<object[]> GetValidCpfs()
        {
            foreach (var cpf in CpfValidatorTestsData.ValidCpfs)
                yield return new object[] { cpf };
        }

        [Theory(DisplayName = "IsValid deve retornar FALSE para entradas inválidas")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]                 // só espaços -> vira vazio após Trim
        [InlineData("123")]                 // tamanho inválido
        [InlineData("1234567890")]          // 10 dígitos
        [InlineData("123456789012")]        // 12 dígitos
        [InlineData("000.000.000-00")]      // dígitos repetidos
        [InlineData("11111111111")]         // dígitos repetidos
        [InlineData("52998224726")]         // DV incorreto (altera último dígito)
        public void IsValid_ReturnsFalse_ForInvalidInputs(string? cpf)
        {
            // Act
            var result = Document.IsValid(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "IsValid lança FormatException quando há letras em string de 11 caracteres (comportamento atual)")]
        public void IsValid_ThrowsFormatException_WhenLettersPresentIn11CharInput()
        {
            var cpfComLetras = "abc123def45"; // 11 caracteres

            Assert.Throws<FormatException>(() => Document.IsValid(cpfComLetras));
        }
    }
}
