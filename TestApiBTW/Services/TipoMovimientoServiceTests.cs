using Application.services;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Moq;

namespace TestApiBTW.Services
{
    public class TipoMovimientoServiceTests
    {
        private readonly Mock<IRepository<TipoMovimiento>> _mockRepo;
        private readonly TipoMovimientoService _service;

        public TipoMovimientoServiceTests()
        {
            _mockRepo = new Mock<IRepository<TipoMovimiento>>();
            _service = new TipoMovimientoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnTipoMovimientos_WhenDataExists()
        {
            // Arrange
            var tipos = new List<TipoMovimiento>
            {
                new TipoMovimiento { IdTipoMovimiento = 1, Descripcion = "Entrada" },
                new TipoMovimiento { IdTipoMovimiento = 2, Descripcion = "Salida" }
            };

            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(tipos);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.NotNull(result.ObjectResponse);
            Assert.Equal(2, result.ObjectResponse.Count);
            Assert.Equal("Entrada", result.ObjectResponse[0].Descripcion);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoData()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<TipoMovimiento>());

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Empty(result.ObjectResponse);
            Assert.Equal("Tipos de movimiento obtenidos correctamente.", result.SuccessMessage);
        }

        [Fact]
        public async Task GetAll_ShouldReturnError_WhenExceptionThrown()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllAsync())
                .ThrowsAsync(new Exception("DB Error"));

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Contains("No se encontraron tipos de movimiento", result.ErrorMessage);
        }
    }
}
