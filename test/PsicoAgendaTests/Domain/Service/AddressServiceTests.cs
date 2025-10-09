

using Domain.Entities;
using Domain.Interface.Repositories;
using Domain.Services;
using Moq;

namespace PsicoAgendaTests.Domain.Service
{
    public class AddressServiceTests
    {
        private readonly Mock<IAddressRepository> _repositoryMock;
        private readonly AddressService _service;

        public AddressServiceTests()
        {
            _repositoryMock = new Mock<IAddressRepository>();
            _service = new AddressService(_repositoryMock.Object);
        }

        [Fact]
        public async Task AddAsync_Should_Add_Address_When_Valid()
        {
            // Arrange
            var address = new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");

            // Act
            await _service.AddAsync(address);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(address, It.IsAny<CancellationToken>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_Should_Throw_When_Null()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _service.AddAsync(null!));
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Address_When_Exists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var address = new Address(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
            _repositoryMock.Setup(r => r.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(address);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
        }


        [Fact]
        public async Task ExistsAsync_Should_Return_True_When_Found()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repositoryMock.Setup(r => r.ExistsAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);

            // Act
            var result = await _service.ExistsAsync(id);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task ExistsAsync_Should_Throw_When_Id_Empty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ExistsAsync(Guid.Empty));
        }

        [Fact]
        public async Task GetAddressesByUserAsync_Should_Return_Addresses()
        {
            // Arrange
            var id = Guid.NewGuid();
            var addresses = new List<Address> { new Address(id, "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP") };
            _repositoryMock.Setup(r => r.GetAddressesByUserAsync(id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(addresses);

            // Act
            var result = await _service.GetAddressesByUserAsync(id);

            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAddressesByUserAsync_Should_Throw_When_Id_Empty()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _service.GetAddressesByUserAsync(Guid.Empty));
        }

        [Fact]
        public async Task Update_Should_Call_Update_And_Save()
        {
            // Arrange
            var address = new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
            _repositoryMock.Setup(r => r.ExistsAsync(address.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(true);

            // Act
            _service.Update(address);
            await Task.Delay(50); // aguarda async void terminar

            // Assert
            _repositoryMock.Verify(r => r.Update(address), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task Delete_Should_Remove_And_Save()
        {
            // Arrange
            var address = new Address(Guid.NewGuid(), "Rua A", 48, "casa 2", "São José do Rio Preto", "15050-230", "SP");
            _repositoryMock.Setup(r => r.GetByIdAsync(address.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(address);

            // Act
            _service.Delete(address, default);
            await Task.Delay(50);

            // Assert
            _repositoryMock.Verify(r => r.Remove(address), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
