using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class HealthCareProfessionalServiceTests
{
    private readonly Mock<IHealthCareProfissionalRepository> _repositoryMock;
    private readonly HealthCareProfissionalService _service;

    public HealthCareProfessionalServiceTests()
    {
        _repositoryMock = new Mock<IHealthCareProfissionalRepository>();
        _service = new HealthCareProfissionalService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se entidade for nula")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new HealthCareProfissional(
            Guid.Parse("d4f2a6b1-8e3a-4f5c-9a0b-cc2f0e1a2b3c"),
            Guid.Parse("f9b1c3a2-d2e3-4f4b-9c7d-0a1b2c3d4e5f"),
            Guid.Parse("f9b1c3a2-d2e3-4a4b-9c7d-0a1b2c3d4e5f"),
            "Dr. Tiago Pereira",
            "https://lattes.cnpq.br/123456789",
            "https://university.edu/tiago-pereira",
            "https://conselho.org/crp/12345",
            ApprovalStatus.Approved,
            AvailabilityStatus.Active
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
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync((HealthCareProfissional?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna entidade se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new HealthCareProfissional(
            Guid.Parse("d4f2a6b1-8e3a-4f5c-9a0b-cc2f0e1a2b3c"),
            Guid.Parse("f9b1c3a2-d2e3-4f4b-9c7d-0a1b2c3d4e5f"),
            Guid.Parse("f9b1c3a2-d2e3-4a4b-9c7d-0a1b2c3d4e5f"),
            "Dr. Tiago Pereira",
            "https://lattes.cnpq.br/123456789",
            "https://university.edu/tiago-pereira",
            "https://conselho.org/crp/12345",
            ApprovalStatus.Approved,
            AvailabilityStatus.Active
        );
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "GetHealthCareProfessionalByAvailabilitiesAsync retorna lista do repositório")]
    public async Task GetByAvailabilitiesAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<HealthCareProfissional> { new HealthCareProfissional() };
        _repositoryMock.Setup(r => r.GetHealthCareProfissionalByAvailabilitiesAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetHealthCareProfissionalByAvailabilitiesAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "GetHealthCareProfessionalBySessionNotesAsync retorna lista do repositório")]
    public async Task GetBySessionNotesAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<HealthCareProfissional> { new HealthCareProfissional() };
        _repositoryMock.Setup(r => r.GetHealthCareProfissionalBySessionNotesAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetHealthCareProfissionalBySessionNotesAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "GetHealthCareProfessionalBySessionScheduleAsync retorna lista do repositório")]
    public async Task GetBySessionScheduleAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<HealthCareProfissional> { new HealthCareProfissional() };
        _repositoryMock.Setup(r => r.GetHealthCareProfissionalBySessionScheduleAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetHealthCareProfissionalBySessionScheduleAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<HealthCareProfissional> { new HealthCareProfissional() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<HealthCareProfissional>, IQueryable<HealthCareProfissional>>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }


    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new HealthCareProfissional(
            Guid.Parse("d4f2a6b1-8e3a-4f5c-9a0b-cc2f0e1a2b3c"),
            Guid.Parse("f9b1c3a2-d2e3-4f4b-9c7d-0a1b2c3d4e5f"),
            Guid.Parse("f9b1c3a2-d2e3-4a4b-9c7d-0a1b2c3d4e5f"),
            "Dr. Tiago Pereira",
            "https://lattes.cnpq.br/123456789",
            "https://university.edu/tiago-pereira",
            "https://conselho.org/crp/12345",
            ApprovalStatus.Approved,
            AvailabilityStatus.Active
        );

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new HealthCareProfissional(
            Guid.Parse("d4f2a6b1-8e3a-4f5c-9a0b-cc2f0e1a2b3c"),
            Guid.Parse("f9b1c3a2-d2e3-4f4b-9c7d-0a1b2c3d4e5f"),
            Guid.Parse("f9b1c3a2-d2e3-4a4b-9c7d-0a1b2c3d4e5f"),
            "Dr. Tiago Pereira",
            "https://lattes.cnpq.br/123456789",
            "https://university.edu/tiago-pereira",
            "https://conselho.org/crp/12345",
            ApprovalStatus.Approved,
            AvailabilityStatus.Active
        );

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}