using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Interface.Repositories;
using Domain.Notifiers;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly Mock<IPatientRepository> _patientRepoMock;
    private readonly Mock<INotifier> _notifierMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _repositoryMock = new Mock<IUserRepository>();
        _patientRepoMock = new Mock<IPatientRepository>();
        _notifierMock = new Mock<INotifier>();
        _service = new UserService(_repositoryMock.Object, _patientRepoMock.Object, _notifierMock.Object);
    }

    [Fact(DisplayName = "AddAsync deve notificar quando e-mail já existir no repositório de paciente")]
    public async Task AddAsync_EmailAlreadyExistsInPatientRepository_ShouldNotify()
    {
        // Arrange
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        );
        _patientRepoMock.Setup(r => r.ExistsByEmailAsync(entity.Email, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        // Act
        await _service.AddAsync(entity);

        // Assert
        _notifierMock.Verify(n => n.Handle(It.Is<Notification>(x =>
            x.Key == nameof(entity.Email) &&
            x.Message == "E-mail já cadastrado."
        )), Times.Once);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "AddAsync deve notificar quando e-mail já existir no repositório de usuário")]
    public async Task AddAsync_EmailAlreadyExistsInUserRepository_ShouldNotify()
    {
        // Arrange
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        );

        _patientRepoMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.ExistsByEmailAsync(entity.Email, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(true);

        // Act
        await _service.AddAsync(entity);

        // Assert
        _notifierMock.Verify(n => n.Handle(It.Is<Notification>(x =>
            x.Key == nameof(entity.Email) &&
            x.Message == "E-mail já cadastrado."
        )), Times.Once);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "AddAsync deve adicionar usuário quando e-mail for válido e não existir")]
    public async Task AddAsync_ValidUser_ShouldAddSuccessfully()
    {
        // Arrange
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        );

        _patientRepoMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);

        // Act
        await _service.AddAsync(entity);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _notifierMock.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
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

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se User não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((User?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna User se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        ); 
        
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<User> { new User() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<User>, IQueryable<User>>>(),
                                               It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }


    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        );

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update deve notificar quando e-mail já existir em paciente ou usuário")]
    public async Task Update_EmailAlreadyExists_ShouldNotify()
    {
        // Arrange
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        );

        _patientRepoMock.Setup(r => r.ExistsByEmailAsync(entity.Email, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        await Task.Run(() => _service.Update(entity));

        // Assert
        _notifierMock.Verify(n => n.Handle(It.Is<Notification>(x =>
            x.Key == nameof(entity.Email) &&
            x.Message == "E-mail já cadastrado."
        )), Times.Once);

        _repositoryMock.Verify(r => r.Update(It.IsAny<User>()), Times.Never);
        _patientRepoMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
    }
    
    [Fact(DisplayName = "Update deve atualizar usuário com sucesso quando dados forem válidos")]
    public async Task Update_ValidUser_ShouldUpdateAndSave()
    {
        // Arrange
        var entity = new User(
            Guid.NewGuid(),
            "Tiago Pereira",
            "tiago.pereira@example.com",
            Gender.Male,
            Guid.Parse("b7a2d5c3-8e1f-47b0-9a20-f3b1e6d8c9a1"),
            "https://cdn.psicoagenda.com/avatars/tiago.png"
        );
        _patientRepoMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        await Task.Run(() => _service.Update(entity));

        // Assert
        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        _notifierMock.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }
}