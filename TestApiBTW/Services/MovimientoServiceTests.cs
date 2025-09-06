using Application.services;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Moq;

namespace TestApiBTW.Services
{
    public class MovimientoServiceTests
    {
        private readonly Mock<IMovimientoRepository> _mockRepo;
        private readonly MovimientoService _service;

        public MovimientoServiceTests()
        {
            _mockRepo = new Mock<IMovimientoRepository>();
            _service = new MovimientoService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnMovimientos_WhenDataExists()
        {
            // Arrange
            var movimientos = new List<Movimientos>
            {
                new Movimientos
                {
                    IdMovimientos = 1,
                    Cantidad = 10,
                    Comentario = "Ingreso",
                    FechaMovimiento = DateTime.UtcNow,
                    IdProducto = 1,
                    IdTipoMovimiento = 2,
                    IdProductoNavigation = new Productos { NombreProducto = "Producto 1" },
                    IdTipoMovimientoNavigation = new TipoMovimiento { Descripcion = "Entrada" }
                }
            };

            _mockRepo.Setup(r => r.GetAllWithIncludes()).ReturnsAsync(movimientos);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.NotNull(result.ObjectResponse);
            Assert.Single(result.ObjectResponse);
            Assert.Equal("Producto 1", result.ObjectResponse.First().producto);
        }

        [Fact]
        public async Task GetAll_ShouldReturnError_WhenNoData()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetAllWithIncludes()).ReturnsAsync(new List<Movimientos>());

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal("No se encontraron movimientos para el cliente especificado.", result.ErrorMessage);
        }

        [Fact]
        public async Task CreateOrder_ShouldReturnError_WhenOrderIsNull()
        {
            // Act
            var result = await _service.CreateOrder(null);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal("Datos de movimiento inválidos.", result.ErrorMessage);
        }

        [Theory]
        [InlineData(5, "Compra inicial")]
        [InlineData(20, "Devolución parcial")]
        [InlineData(100, "Inventario completo")]
        public async Task CreateOrder_ShouldCreateMovimiento_WithValidData(int cantidad, string comentario)
        {
            // Arrange
            var dto = new MovimientoDTO
            {
                cantidad = cantidad,
                comentario = comentario,
                fechaMovimiento = DateTime.UtcNow,
                idProducto = 1,
                idTipoMovimiento = 2
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Movimientos>()))
                .ReturnsAsync((Movimientos m) => m);

            // Act
            var result = await _service.CreateOrder(dto);

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.NotNull(result.ObjectResponse);
            Assert.Equal(cantidad, result.ObjectResponse.Cantidad);
            Assert.Equal(comentario, result.ObjectResponse.Comentario);
        }

        [Fact]
        public async Task CreateOrder_ShouldHandleException()
        {
            // Arrange
            var dto = new MovimientoDTO
            {
                cantidad = 1,
                comentario = "Error test",
                fechaMovimiento = DateTime.UtcNow,
                idProducto = 1,
                idTipoMovimiento = 2
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Movimientos>()))
                .ThrowsAsync(new Exception("DB Error"));

            // Act
            var result = await _service.CreateOrder(dto);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Contains("Error al crear el movimiento", result.ErrorMessage);
        }
    }
}
