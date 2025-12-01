using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class AvailabilitieServiceTests
{
    private readonly Mock<IAvailabilitieRepository> _repositoryMock;
    private readonly AvailabilitieService _service; 

    public AvailabilitieServiceTests()
    {
        _repositoryMock = new Mock<IAvailabilitieRepository>();
        _service = new AvailabilitieService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se entidade for nula")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new Availabilitie(Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e0a1f9d22"),
            Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e2a1f9d22"), 
            Guid.Parse("c7a3a9f1-9f3c-4a5e-b2e0-d45b3f8c1a44"),
            DateTime.UtcNow.AddDays(1).AddHours(14),
            TimeSpan.FromMinutes(50),
            Guid.Parse("e1a5b7f9-9a2d-4b8c-b2e5-8f6a3d1c4b77"),
            Source.Internal,
            TypeAvailabilitie.Online,
            "Clínica Central - Sala 203",
            "https://meet.google.com/psicoagenda-session", 
            true);

        await _service.AddAsync(entity);

        _repositoryMock.Verify(r => r.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "ExistsAsync lança ArgumentException se id for Guid.Empty")]
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
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((Availabilitie?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna entidade se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new Availabilitie(id,
            Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e2a1f9d22"),
            Guid.Parse("c7a3a9f1-9f3c-4a5e-b2e0-d45b3f8c1a44"),
            DateTime.UtcNow.AddDays(1).AddHours(14),
            TimeSpan.FromMinutes(50),
            Guid.Parse("e1a5b7f9-9a2d-4b8c-b2e5-8f6a3d1c4b77"),
            Source.Internal,
            TypeAvailabilitie.Online,
            "Clínica Central - Sala 203",
            "https://meet.google.com/psicoagenda-session",
            true); _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }


    [Fact(DisplayName = "IsBookedAsync retorna lista do repositório")]
    public async Task IsBookedAsync_ReturnsList()
    {
        var list = new List<Availabilitie> { new Availabilitie() };
        _repositoryMock.Setup(r => r.IsBookedAsync(It.IsAny<CancellationToken>())).ReturnsAsync(list);

        var result = await _service.IsBookedAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "IsNotBookedAsync retorna lista do repositório")]
    public async Task IsNotBookedAsync_ReturnsList()
    {
        var list = new List<Availabilitie> { new Availabilitie() };
        _repositoryMock.Setup(r => r.IsNotBookedAsync(It.IsAny<CancellationToken>())).ReturnsAsync(list);

        var result = await _service.IsNotBookedAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<Availabilitie> { new Availabilitie() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<Availabilitie>, IQueryable<Availabilitie>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new Availabilitie(Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e0a1f9d22"),
            Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e2a1f9d22"),
            Guid.Parse("c7a3a9f1-9f3c-4a5e-b2e0-d45b3f8c1a44"),
            DateTime.UtcNow.AddDays(1).AddHours(14),
            TimeSpan.FromMinutes(50),
            Guid.Parse("e1a5b7f9-9a2d-4b8c-b2e5-8f6a3d1c4b77"),
            Source.Internal,
            TypeAvailabilitie.Online,
            "Clínica Central - Sala 203",
            "https://meet.google.com/psicoagenda-session",
            true);
        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new Availabilitie(Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e0a1f9d22"),
            Guid.Parse("b8b8c1b2-5c3e-4c6d-97e8-7f4e2a1f9d22"),
            Guid.Parse("c7a3a9f1-9f3c-4a5e-b2e0-d45b3f8c1a44"),
            DateTime.UtcNow.AddDays(1).AddHours(14),
            TimeSpan.FromMinutes(50),
            Guid.Parse("e1a5b7f9-9a2d-4b8c-b2e5-8f6a3d1c4b77"),
            Source.Internal,
            TypeAvailabilitie.Online,
            "Clínica Central - Sala 203",
            "https://meet.google.com/psicoagenda-session",
            true);
        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}