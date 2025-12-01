using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class ConsentServiceTests
{
    private readonly Mock<IConsentRepository> _repositoryMock;
    private readonly ConsentService _service;

    public ConsentServiceTests()
    {
        _repositoryMock = new Mock<IConsentRepository>();
        _service = new ConsentService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se entidade for nula")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new Consent(
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            "Termo de Consentimento",
            DateTime.Now,
            "v2.1");
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
        _repositoryMock.Setup(r => r.ExistsAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _service.ExistsAsync(id);

        Assert.True(result);
    }

    [Fact(DisplayName = "GetByIdAsync lança ArgumentException se id for vazio")]
    public async Task GetByIdAsync_EmptyId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetByIdAsync(Guid.Empty));
    }

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se entidade não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Consent?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna entidade se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new Consent(
            id,
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            "Termo de Consentimento",
            DateTime.Now,
            "v2.1"); _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<Consent> { new Consent() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<Consent>, IQueryable<Consent>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new Consent(
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            "Termo de Consentimento",
            DateTime.Now,
            "v2.1");

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new Consent(
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("b1c2d3e4-f5a6-789b-9c0d-e1f2a3b4c5d6"),
            "Termo de Consentimento",
            DateTime.Now, 
            "v2.1");

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}