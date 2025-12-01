using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class SpecialityServiceTests
{
    private readonly Mock<ISpecialityRepository> _repositoryMock;
    private readonly SpecialityService _service;

    public SpecialityServiceTests()
    {
        _repositoryMock = new Mock<ISpecialityRepository>();
        _service = new SpecialityService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se Speciality for nulo")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new Speciality(Guid.NewGuid(), "Psicologia Clínica"); 

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

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se Speciality não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Speciality?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna Speciality se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new Speciality(Guid.NewGuid(), "Psicologia Clínica");
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<Speciality> { new Speciality() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<Speciality>, IQueryable<Speciality>>>(),
                                               It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new Speciality(Guid.NewGuid(), "Psicologia Clínica");

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new Speciality(Guid.NewGuid(), "Psicologia Clínica");

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}