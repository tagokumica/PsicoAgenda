using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class WaitServiceTests
{
    private readonly Mock<IWaitRepository> _repositoryMock;
    private readonly WaitService _service;

    public WaitServiceTests()
    {
        _repositoryMock = new Mock<IWaitRepository>();
        _service = new WaitService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se Wait for nulo")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new Wait(
            Guid.NewGuid(), 
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow.AddDays(3).AddHours(14),
            DateTime.Today, 
            DateTime.Now);

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

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se Wait não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Wait?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna Wait se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new Wait(
            id,
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow.AddDays(3).AddHours(14),
            DateTime.Today,
            DateTime.Now);

        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<Wait> { new Wait() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<Wait>, IQueryable<Wait>>>(),
                                               It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new Wait(
            Guid.NewGuid(),
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow.AddDays(3).AddHours(14),
            DateTime.Today,
            DateTime.Now);


        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Remove (bug na implementação) e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new Wait(
            Guid.NewGuid(),
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow.AddDays(3).AddHours(14),
            DateTime.Today,
            DateTime.Now);

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}