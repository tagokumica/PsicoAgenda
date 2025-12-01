using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class TermsOfUseServiceTests
{
    private readonly Mock<ITermOfUseRepository> _repositoryMock;
    private readonly TermOfUseService _service;

    public TermsOfUseServiceTests()
    {
        _repositoryMock = new Mock<ITermOfUseRepository>();
        _service = new TermOfUseService(_repositoryMock.Object);
    }


    [Fact(DisplayName = "AddAsync lança ArgumentNullException se TermsOfUse for nulo")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new TermsOfUse(
            Guid.NewGuid(), 
            """
                     Ao utilizar a plataforma PsicoAgenda, você concorda com os termos de confidencialidade, 
                     proteção de dados e boas práticas éticas no atendimento psicológico.
                     """,
            "Termos de Uso e Política de Privacidade",
            "v1.2",
            DateTime.Now
        );

        await _service.AddAsync(entity);

        _repositoryMock.Verify(r => r.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "ExistsAsync lança ArgumentException se Id for Guid.Empty")]
    public async Task ExistsAsync_EmptyId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.ExistsAsync(Guid.Empty));
    }

    [Fact(DisplayName = "ExistsAsync retorna valor do repositório")]
    public async Task ExistsAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.ExistsAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(true);

        var result = await _service.ExistsAsync(id);

        Assert.True(result);
    }

    [Fact(DisplayName = "GetByIdAsync lança ArgumentException se Id for Guid.Empty")]
    public async Task GetByIdAsync_EmptyId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByIdAsync(Guid.Empty));
    }

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se TermsOfUse não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((TermsOfUse?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna TermsOfUse se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new TermsOfUse(
            Guid.NewGuid(), 
            """
            Ao utilizar a plataforma PsicoAgenda, você concorda com os termos de confidencialidade, 
            proteção de dados e boas práticas éticas no atendimento psicológico.
            """,
            "Termos de Uso e Política de Privacidade",
            "v1.2",
            DateTime.Now
        );
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "GetTermsOfUseByTermsAcceptanceAsync retorna lista do repositório")]
    public async Task GetByTermsAcceptanceAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<TermsOfUse> { new TermsOfUse() };
        _repositoryMock.Setup(r => r.GetTermsOfUseByTermsAcceptanceAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetTermsOfUseByTermsAcceptanceAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<TermsOfUse> { new TermsOfUse() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<TermsOfUse>, IQueryable<TermsOfUse>>>(),
                                               It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new TermsOfUse(
            Guid.NewGuid(), 
            """
            Ao utilizar a plataforma PsicoAgenda, você concorda com os termos de confidencialidade, 
            proteção de dados e boas práticas éticas no atendimento psicológico.
            """,
            "Termos de Uso e Política de Privacidade",
            "v1.2",
            DateTime.Now
        );

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new TermsOfUse(
            Guid.NewGuid(), 
            """
            Ao utilizar a plataforma PsicoAgenda, você concorda com os termos de confidencialidade, 
            proteção de dados e boas práticas éticas no atendimento psicológico.
            """,
            "Termos de Uso e Política de Privacidade",
            "v1.2",
            DateTime.Now
        );

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}