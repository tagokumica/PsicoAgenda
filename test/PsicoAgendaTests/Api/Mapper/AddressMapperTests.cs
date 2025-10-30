using AutoMapper;
using Domain.Entities;
using Domain.Interface.Services;
using Moq;
using PsicoAgendaAPI.AutoMapper.Mapper;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaTests.Api.Mapper;

public class AddressMapperTests
{
    private readonly Mock<IAddressService> _serviceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly AddressMapper _app;

    public AddressMapperTests()
    {
        _serviceMock = new Mock<IAddressService>();
        _mapperMock = new Mock<IMapper>();
        _app = new AddressMapper(_mapperMock.Object, _serviceMock.Object);
    }

    [Fact(DisplayName = "AddAsync chama mapper e service corretamente")]
    public async Task AddAsync_Valid_CallsService()
    {
        var vm = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
        var entity = new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");

        _mapperMock.Setup(m => m.Map<Address>(vm)).Returns(entity);

        await _app.AddAsync(vm);

        _mapperMock.Verify(m => m.Map<Address>(vm), Times.Once);
        _serviceMock.Verify(s => s.AddAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact(DisplayName = "Delete chama mapper e service corretamente")]
    public void Delete_CallsService()
    {
        var vm = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
        var entity = new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");

        _mapperMock.Setup(m => m.Map<Address>(vm)).Returns(entity);

        _app.Delete(vm, CancellationToken.None);

        _mapperMock.Verify(m => m.Map<Address>(vm), Times.Once);
        _serviceMock.Verify(s => s.Delete(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = "GetByIdAsync retorna AddressViewModel mapeado")]
    public async Task GetByIdAsync_ReturnsViewModel()
    {
        var id = Guid.NewGuid();
        var vm = new AddressViewModel(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
        var entity = new Address(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");

        _serviceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>())).ReturnsAsync(entity);
        _mapperMock.Setup(m => m.Map<AddressViewModel>(entity)).Returns(vm);

        var result = await _app.GetByIdAsync(id);

        Assert.Equal(vm.Id, result.Id);
        _mapperMock.Verify(m => m.Map<AddressViewModel>(entity), Times.Once);
    }

    [Fact(DisplayName = "GetAddressByLocationAsync retorna lista mapeada")]
    public async Task GetAddressByLocationAsync_ReturnsMappedList()
    {
        var id = Guid.NewGuid();
        var entities = new List<Address> { new Address(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };
        var vms = new List<AddressViewModel> { new AddressViewModel(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };

        _serviceMock.Setup(s => s.GetAddressByLocationAsync(id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<IEnumerable<AddressViewModel>>(entities))
                    .Returns(vms);

        var result = await _app.GetAddressByLocationAsync(id);

        Assert.Single(result);
        _mapperMock.Verify(m => m.Map<IEnumerable<AddressViewModel>>(entities), Times.Once);
    }

    [Fact(DisplayName = "GetAddressesByUserAsync retorna lista mapeada")]
    public async Task GetAddressesByUserAsync_ReturnsMappedList()
    {
        var id = Guid.NewGuid();
        var entities = new List<Address> { new Address(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };
        var vms = new List<AddressViewModel> { new AddressViewModel(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };

        _serviceMock.Setup(s => s.GetAddressesByUserAsync(id, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<IEnumerable<AddressViewModel>>(entities))
                    .Returns(vms);

        var result = await _app.GetAddressesByUserAsync(id);

        Assert.Single(result);
    }

    [Fact(DisplayName = "ListAsync retorna lista mapeada sem query")]
    public async Task ListAsync_WithoutQuery_ReturnsMapped()
    {
        var entities = new List<Address> { new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };
        var vms = new List<AddressViewModel> { new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };

        _serviceMock.Setup(s => s.ListAsync(null, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<AddressViewModel>>(entities))
                    .Returns(vms);

        var result = await _app.ListAsync();

        Assert.Equal(vms.Count, result.Count);
    }

    [Fact(DisplayName = "ListAsync aplica query sobre ViewModels")]
    public async Task ListAsync_WithQuery_AppliesFilter()
    {
        var entities = new List<Address>
        {
            new Address(Guid.NewGuid(), "Rua B", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP"),
            new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP")
        };
        var vms = new List<AddressViewModel>
        {
            new AddressViewModel(Guid.NewGuid(), "Rua B", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP"),
            new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP")
        };

        _serviceMock.Setup(s => s.ListAsync(null, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(entities);
        _mapperMock.Setup(m => m.Map<List<AddressViewModel>>(entities))
                    .Returns(vms);

        var result = await _app.ListAsync(q => q.Where(x => x.State == "SP"));

        Assert.Equal("SP", result.First().State);
    }

    [Fact(DisplayName = "Update chama mapper e service corretamente")]
    public void Update_CallsService()
    {
        var vm = new AddressViewModel(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
        var entity = new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "Campinas");

        _mapperMock.Setup(m => m.Map<Address>(vm)).Returns(entity);

        _app.Update(vm);

        _mapperMock.Verify(m => m.Map<Address>(vm), Times.Once);
        _serviceMock.Verify(s => s.Update(entity), Times.Once);
    }

}