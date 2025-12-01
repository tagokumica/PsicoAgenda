using Domain.Entities;
using Domain.Entities.Enum;
using Domain.Entities.ValueObject;
using Domain.Interface.Repositories;
using Domain.Notifiers;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class PatientServiceTests
{
    private readonly Mock<IPatientRepository> _repositoryMock;
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<INotifier> _notifierMock;

    private readonly PatientService _service;

    public PatientServiceTests()
    {
        _repositoryMock = new Mock<IPatientRepository>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _notifierMock = new Mock<INotifier>();
        _service = new PatientService(_repositoryMock.Object, _notifierMock.Object, _userRepositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync deve notificar quando CPF for inválido")]
    public async Task AddAsync_InvalidCpf_ShouldNotifyAndNotAdd()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "invalid-cpf",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );

        MockDocumentIsValid(entity.Cpf);

        // Act
        await _service.AddAsync(entity);

        // Assert
        _notifierMock.Verify(n => n.Handle(It.Is<Notification>(x =>
            x.Key == nameof(entity.Cpf) &&
            x.Message == "CPF inválido."
        )), Times.Once);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "AddAsync deve notificar quando CPF já estiver cadastrado")]
    public async Task AddAsync_CpfAlreadyExists_ShouldNotify()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678909",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );
        MockDocumentIsValid(entity.Cpf);
        _repositoryMock.Setup(r => r.ExistsByCpfAsync(entity.Cpf, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

        // Act
        await _service.AddAsync(entity);

        // Assert
        _notifierMock.Verify(n => n.Handle(It.Is<Notification>(x =>
            x.Key == nameof(entity.Cpf) &&
            x.Message == "CPF já cadastrado."
        )), Times.Once);

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "AddAsync deve notificar quando e-mail já existir no repositório de paciente ou usuário")]
    public async Task AddAsync_EmailAlreadyExists_ShouldNotify()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678909",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );
        MockDocumentIsValid(entity.Cpf);
        _repositoryMock.Setup(r => r.ExistsByCpfAsync(entity.Cpf, It.IsAny<CancellationToken>()))
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

        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact(DisplayName = "AddAsync deve cadastrar paciente com sucesso quando dados forem válidos")]
    public async Task AddAsync_ValidPatient_ShouldAddSuccessfully()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678909",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );
        MockDocumentIsValid(entity.Cpf);
        _repositoryMock.Setup(r => r.ExistsByCpfAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
        _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(false);
        _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(false);

        // Act
        await _service.AddAsync(entity);

        // Assert
        _repositoryMock.Verify(r => r.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _notifierMock.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    [Fact(DisplayName = "AddAsync deve relançar ArgumentNullException quando ocorrer no repositório")]
    public async Task AddAsync_WhenRepositoryThrowsArgumentNullException_ShouldRethrow()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678909",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );
        MockDocumentIsValid(entity.Cpf);

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Patient>(), It.IsAny<CancellationToken>()))
                        .ThrowsAsync(new ArgumentNullException("Paciente nulo."));

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(entity));
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

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se Patient não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Patient?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna Patient se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678901",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );
        
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "GetPatientByAvailabilitiesAsync retorna lista do repositório")]
    public async Task GetByAvailabilitiesAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<Patient> { new Patient() };
        _repositoryMock.Setup(r => r.GetPatientByAvailabilitiesAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetPatientByAvailabilitiesAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "GetPatientByConsentAsync retorna lista do repositório")]
    public async Task GetByConsentAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<Patient> { new Patient() };
        _repositoryMock.Setup(r => r.GetPatientByConsentAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetPatientByConsentAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "GetPatientByWaitsAsync retorna lista do repositório")]
    public async Task GetByWaitsAsync_ReturnsList()
    {
        var id = Guid.NewGuid();
        var list = new List<Patient> { new Patient() };
        _repositoryMock.Setup(r => r.GetPatientByWaitsAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetPatientByWaitsAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<Patient> { new Patient() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<Patient>, IQueryable<Patient>>>(), It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678901",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update deve notificar quando e-mail já existir em repositório de paciente ou usuário")]
    public async Task Update_EmailAlreadyExists_ShouldNotifyAndReturn()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678901",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );

        _repositoryMock.Setup(r => r.ExistsByEmailAsync(entity.Email, CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        await Task.Run(() => _service.Update(entity)); // o método é async void, por isso usamos Task.Run

        // Assert
        _notifierMock.Verify(n => n.Handle(It.Is<Notification>(x =>
            x.Key == nameof(entity.Email) &&
            x.Message == "E-mail já cadastrado."
        )), Times.Once);

        _repositoryMock.Verify(r => r.Update(It.IsAny<Patient>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Update deve atualizar paciente quando dados forem válidos")]
    public async Task Update_ValidPatient_ShouldUpdateAndSave()
    {
        // Arrange
        var entity = new Patient(
            Guid.NewGuid(),
            "Ana Souza",
            "ana.souza@example.com",
            new DateTime(1992, 8, 14),
            "12345678901",
            "Paciente com histórico de ansiedade leve.",
            Gender.Female,
            "+55 (11) 91234-5678"
        );
        _repositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                        .ReturnsAsync(false);
        _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(It.IsAny<string>(), CancellationToken.None))
                     .ReturnsAsync(false);

        // Act
        await Task.Run(() => _service.Update(entity));

        // Assert
        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(CancellationToken.None), Times.Once);
        _notifierMock.Verify(n => n.Handle(It.IsAny<Notification>()), Times.Never);
    }

    private static void MockDocumentIsValid(string document)
    {
        Document.IsValid(document);
    }

}