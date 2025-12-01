using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service.Service;

public class TermsAcceptanceServiceTests
{
    private readonly Mock<ITermsAcceptanceRepository> _repositoryMock;
    private readonly TermsAcceptanceService _service;

    public TermsAcceptanceServiceTests()
    {
        _repositoryMock = new Mock<ITermsAcceptanceRepository>();
        _service = new TermsAcceptanceService(_repositoryMock.Object);
    }

    [Fact(DisplayName = "AddAsync lança ArgumentNullException se TermsAcceptance for nulo")]
    public async Task AddAsync_Null_Throws()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
    }

    [Fact(DisplayName = "AddAsync adiciona e salva corretamente")]
    public async Task AddAsync_Valid_CallsRepository()
    {
        var entity =  new TermsAcceptance(
            Guid.NewGuid(), 
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow,
            true,
            """
                     Termos de Uso e Política de Privacidade v1.2
                     O usuário concorda com o tratamento ético e confidencial de seus dados pessoais.
                     """
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

    [Fact(DisplayName = "GetByIdAsync lança KeyNotFoundException se TermsAcceptance não existir")]
    public async Task GetByIdAsync_NotFound_Throws()
    {
        var id = Guid.NewGuid();
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((TermsAcceptance?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(id));
    }

    [Fact(DisplayName = "GetByIdAsync retorna TermsAcceptance se existir")]
    public async Task GetByIdAsync_Valid_Returns()
    {
        var id = Guid.NewGuid();
        var entity = new TermsAcceptance(
            id,
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow,
            true,
            """
            Termos de Uso e Política de Privacidade v1.2
            O usuário concorda com o tratamento ético e confidencial de seus dados pessoais.
            """
        );
        _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.GetByIdAsync(id);

        Assert.Equal(entity, result);
    }


    [Fact(DisplayName = "IsAgreedAsync lança ArgumentException se userId for vazio")]
    public async Task IsAgreedAsync_EmptyUserId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.IsAgreedAsync(Guid.Empty, Guid.NewGuid()));
    }

    [Fact(DisplayName = "IsAgreedAsync lança ArgumentException se termsOfUseId for vazio")]
    public async Task IsAgreedAsync_EmptyTermsId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.IsAgreedAsync(Guid.NewGuid(), Guid.Empty));
    }

    [Fact(DisplayName = "IsAgreedAsync retorna TermsAcceptance do repositório")]
    public async Task IsAgreedAsync_Valid_Returns()
    {
        var userId = Guid.NewGuid();
        var termsId = Guid.NewGuid();
        var entity = new TermsAcceptance(
            Guid.NewGuid(),
            userId,
            termsId,
            DateTime.UtcNow,
            true,
            """
            Termos de Uso e Política de Privacidade v1.2
            O usuário concorda com o tratamento ético e confidencial de seus dados pessoais.
            """
        );

        _repositoryMock.Setup(r => r.IsAgreedAsync(userId, termsId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.IsAgreedAsync(userId, termsId);

        Assert.Equal(entity, result);
    }

    [Fact(DisplayName = "DisagreeAsync lança ArgumentException se userId for vazio")]
    public async Task IsNotAgreedAsync_EmptyUserId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.IsNotAgreedAsync(Guid.Empty, Guid.NewGuid()));
    }

    [Fact(DisplayName = "DisagreeAsync lança ArgumentException se termsOfUseId for vazio")]
    public async Task DisagreeAsync_EmptyTermsId_Throws()
    {
        await Assert.ThrowsAsync<ArgumentException>(() => _service.IsNotAgreedAsync(Guid.NewGuid(), Guid.Empty));
    }

    [Fact(DisplayName = "DisagreeAsync retorna TermsAcceptance do repositório")]
    public async Task DisagreeAsync_Valid_Returns()
    {
        var userId = Guid.NewGuid();
        var termsId = Guid.NewGuid();
        var entity = new TermsAcceptance(
            Guid.NewGuid(),
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow,
            false,
            """
            Termos de Uso e Política de Privacidade v1.2
            O usuário concorda com o tratamento ético e confidencial de seus dados pessoais.
            """
        );

        _repositoryMock.Setup(r => r.IsAgreedAsync(userId, termsId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(entity);

        var result = await _service.IsNotAgreedAsync(userId, termsId);

        Assert.Equal(entity, result);
    }


    [Fact(DisplayName = "GetIsAgreedAsync retorna lista do repositório")]
    public async Task GetIsAgreedAsync_ReturnsList()
    {
        var list = new List<TermsAcceptance> { new TermsAcceptance() };
        _repositoryMock.Setup(r => r.GetIsAgreedAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetIsAgreedAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "GetDisagreeAsync retorna lista do repositório")]
    public async Task GetDisagreeAsync_ReturnsList()
    {
        var list = new List<TermsAcceptance> { new TermsAcceptance() };
        _repositoryMock.Setup(r => r.GetIsNotAgreedAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.GetIsNotAgreedAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "ListAsync retorna lista do repositório")]
    public async Task ListAsync_ReturnsList()
    {
        var list = new List<TermsAcceptance> { new TermsAcceptance() };
        _repositoryMock.Setup(r => r.ListAsync(It.IsAny<Func<IQueryable<TermsAcceptance>, IQueryable<TermsAcceptance>>>(),
                                               It.IsAny<CancellationToken>()))
                       .ReturnsAsync(list);

        var result = await _service.ListAsync();

        Assert.Single(result);
    }

    [Fact(DisplayName = "Delete chama Remove e SaveChangesAsync")]
    public void Delete_CallsRepository()
    {
        var entity = new TermsAcceptance(
            Guid.NewGuid(),
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow,
            true,
            """
            Termos de Uso e Política de Privacidade v1.2
            O usuário concorda com o tratamento ético e confidencial de seus dados pessoais.
            """
        );

        _service.Delete(entity, CancellationToken.None);

        _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "Update chama Update e SaveChangesAsync")]
    public void Update_CallsRepository()
    {
        var entity = new TermsAcceptance(
            Guid.NewGuid(),
            Guid.Parse("b3a2f1c4-9e8d-4f7b-a6c5-1d2e3f4a5b6c"),
            Guid.Parse("c7a1b2c3-d4e5-6789-0abc-def123456789"),
            DateTime.UtcNow,
            true,
            """
            Termos de Uso e Política de Privacidade v1.2
            O usuário concorda com o tratamento ético e confidencial de seus dados pessoais.
            """
        );

        _service.Update(entity);

        _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}