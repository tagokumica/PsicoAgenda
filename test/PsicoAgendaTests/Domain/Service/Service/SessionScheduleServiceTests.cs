using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class SessionScheduleServiceTests
{
    private readonly Mock<ISessionScheduleRepository> _repositoryMock;
    private readonly SessionScheduleService _service;

    public SessionScheduleServiceTests()
    {
        _repositoryMock = new Mock<ISessionScheduleRepository>();
        _service = new SessionScheduleService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se SessionSchedule for nulo")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity = new SessionSchedule(
            Guid.NewGuid(), 
            Guid.Parse("b2c3d4e5-f6a7-489b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
            Guid.Parse("c3d4e5f6-a7b8-9c0d-e1f2-a3b4c5d67890"),
            DateTime.UtcNow.AddDays(2).AddHours(10),
            Status.Sheduled,
            TimeSpan.FromMinutes(50)
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

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se SessionSchedule não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((SessionSchedule?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna SessionSchedule se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new SessionSchedule(
            id,
            Guid.Parse("b2c3d4e5-f6a7-489b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
            Guid.Parse("c3d4e5f6-a7b8-9c0d-e1f2-a3b4c5d67890"),
            DateTime.UtcNow.AddDays(2).AddHours(10),
            Status.Sheduled,
            TimeSpan.FromMinutes(50)
        );
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "GetSessionScheduleBySessionNotesAsync retorna lista do repositório")]
    public async Task GetBySessionNotesAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<SessionSchedule> { new SessionSchedule() };
        _repositoryMock.Setup(r => r.GetSessionScheduleBySessionNotesAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetSessionScheduleBySessionNotesAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "GetSessionScheduleByWaitsAsync retorna lista do repositório")]
    public async Task GetByWaitsAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<SessionSchedule> { new SessionSchedule() };
        _repositoryMock.Setup(r => r.GetSessionScheduleByWaitsAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetSessionScheduleByWaitsAsync(id);

        Assert.Single(result);
    }
    
    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<SessionSchedule> { new SessionSchedule() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<SessionSchedule>, IQueryable<SessionSchedule>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new SessionSchedule(
            Guid.NewGuid(),
            Guid.Parse("b2c3d4e5-f6a7-489b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
            Guid.Parse("c3d4e5f6-a7b8-9c0d-e1f2-a3b4c5d67890"),
            DateTime.UtcNow.AddDays(2).AddHours(10),
            Status.Sheduled,
            TimeSpan.FromMinutes(50)
        );


        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new SessionSchedule(
            Guid.NewGuid(),
            Guid.Parse("b2c3d4e5-f6a7-489b-9c0d-e1f2a3b4c5d6"),
            Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
            Guid.Parse("c3d4e5f6-a7b8-9c0d-e1f2-a3b4c5d67890"),
            DateTime.UtcNow.AddDays(2).AddHours(10),
            Status.Sheduled,
            TimeSpan.FromMinutes(50)
        );

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "GetSessionScheduleByDurationCountAsync deve retornar contagem correta")]
    public async Task GetSessionScheduleByDurationCountAsync_ShouldReturnExpectedCount()
    {
        // Arrange
        var expectedCount = 5;
        var duration = TimeSpan.FromMinutes(60);
        var cancellation = CancellationToken.None;

        _repositoryMock
            .Setup(r => r.GetSessionScheduleByDurationCountAsync(duration, cancellation))
            .ReturnsAsync(expectedCount);

        // Act
        var result = await _service.GetSessionScheduleByDurationCountAsync(duration, cancellation);

        // Assert
        Assert.Equal(expectedCount, result);
        _repositoryMock.Verify(r => r.GetSessionScheduleByDurationCountAsync(duration, cancellation), Times.Once);
    }

    [Fact(DisplayName = "GetSessionScheduleBySessionNotesAsync deve retornar lista de sessões")]
    public async Task GetSessionScheduleBySessionNotesAsync_ShouldReturnSessionList()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var expectedSessions = new List<SessionSchedule>
        {
            new SessionSchedule(
                sessionId,
                Guid.Parse("b2c3d4e5-f6a7-489b-9c0d-e1f2a3b4c5d6"),
                Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                Guid.Parse("c3d4e5f6-a7b8-9c0d-e1f2-a3b4c5d67890"),
                DateTime.UtcNow.AddDays(2).AddHours(10),
                Status.Sheduled,
                TimeSpan.FromMinutes(50)
            ),

            new SessionSchedule(
                sessionId,
                Guid.Parse("b2c3d4e5-f6a7-489b-9c0d-e1f2a3b4c5d6"),
                Guid.Parse("a1b2c3d4-e5f6-7890-1234-56789abcdef0"),
                Guid.Parse("c3d4e5f6-a7b8-9c0d-e1f2-a3b4c5d67890"),
                DateTime.UtcNow.AddDays(2).AddHours(20),
                Status.Sheduled,
                TimeSpan.FromMinutes(30)
            )

        };

        _repositoryMock
            .Setup(r => r.GetSessionScheduleBySessionNotesAsync(sessionId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSessions);

        // Act
        var result = await _service.GetSessionScheduleBySessionNotesAsync(sessionId);

        // Assert
        Assert.NotNull(expectedSessions);
        Assert.Equal(2, result.Count());
        _repositoryMock.Verify(r => r.GetSessionScheduleBySessionNotesAsync(sessionId, It.IsAny<CancellationToken>()), Times.Once);
    }
}