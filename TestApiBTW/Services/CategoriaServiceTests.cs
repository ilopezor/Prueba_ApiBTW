using Application.services;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Moq;

namespace TestApiBTW.Services
{
    public class CategoriaServiceTests
    {
        private readonly Mock<IRepository<Categoria>> _categoriaRepositoryMock;
        private readonly CategoriaService _service;

        public CategoriaServiceTests()
        {
            _categoriaRepositoryMock = new Mock<IRepository<Categoria>>();
            _service = new CategoriaService(_categoriaRepositoryMock.Object);
        }

        // ----------------------------
        // SaveData
        // ----------------------------
        [Fact]
        public async Task SaveData_ShouldReturnError_WhenCategoriaIsNull()
        {
            var result = await _service.SaveData(null);

            Assert.False(result.OperationSuccess);
            Assert.Equal("Datos de categoría inválidos.", result.ErrorMessage);
        }

        [Fact]
        public async Task SaveData_ShouldSave_WhenCategoriaIsValid()
        {
            // Arrange
            var categoriaDto = new CategoriaDTO { NombreCategoria = "Electrónica" };

            _categoriaRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Categoria>());

            _categoriaRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Categoria>()))
                .ReturnsAsync(new Categoria
                {
                    IdCategoria = 1,
                    NombreCategoria = "Electrónica",
                    Estado = true
                });

            // Act
            var result = await _service.SaveData(categoriaDto);

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Equal("Categoría guardada correctamente.", result.SuccessMessage);
            Assert.Equal("Electrónica", result.ObjectResponse.NombreCategoria);
        }

        // ----------------------------
        // GetAll
        // ----------------------------
        [Fact]
        public async Task GetAll_ShouldReturnActiveCategories()
        {
            // Arrange
            var categorias = new List<Categoria>
            {
                new Categoria { IdCategoria = 1, NombreCategoria = "Hogar", Estado = true },
                new Categoria { IdCategoria = 2, NombreCategoria = "Electrónica", Estado = false }
            };

            _categoriaRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(categorias);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Single(result.ObjectResponse); 
            Assert.Equal("Hogar", result.ObjectResponse[0].NombreCategoria);
        }

        // ----------------------------
        // UpdateData
        // ----------------------------
        [Theory]
        [InlineData(null, "Datos de categoría inválidos.")]
        [InlineData(0, "Datos de categoría inválidos.")]
        [InlineData(-1, "Datos de categoría inválidos.")]
        public async Task UpdateData_ShouldReturnError_WhenCategoriaIsInvalid(int? id, string expectedError)
        {
            // Arrange
            var dto = id == null ? null : new CategoriaDTO { IdCategoria = id.Value };

            // Act
            var result = await _service.UpdateData(dto);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal(expectedError, result.ErrorMessage);
        }

        [Fact]
        public async Task UpdateData_ShouldReturnError_WhenCategoriaNotFound()
        {
            // Arrange
            var dto = new CategoriaDTO { IdCategoria = 99, NombreCategoria = "Nueva" };

            _categoriaRepositoryMock.Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Categoria)null);

            // Act
            var result = await _service.UpdateData(dto);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal("Categoría no encontrada.", result.ErrorMessage);
        }

        [Fact]
        public async Task UpdateData_ShouldUpdate_WhenCategoriaExists()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 1, NombreCategoria = "Vieja" };
            var dto = new CategoriaDTO { IdCategoria = 1, NombreCategoria = "Nueva" };

            _categoriaRepositoryMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(categoria);

            _categoriaRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Categoria>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.UpdateData(dto);

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Equal("Nueva", result.ObjectResponse.NombreCategoria);
        }

        // ----------------------------
        // DeleteData
        // ----------------------------
        [Fact]
        public async Task DeleteData_ShouldReturnError_WhenCategoriaNotFound()
        {
            _categoriaRepositoryMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync((Categoria)null);

            var result = await _service.DeleteData(1);

            Assert.False(result.OperationSuccess);
            Assert.Equal("Categoría no encontrada.", result.ErrorMessage);
        }

        [Fact]
        public async Task DeleteData_ShouldSetEstadoFalse_WhenCategoriaExists()
        {
            // Arrange
            var categoria = new Categoria { IdCategoria = 1, NombreCategoria = "Hogar", Estado = true };

            _categoriaRepositoryMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(categoria);

            _categoriaRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Categoria>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteData(1);

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.False(result.ObjectResponse.Estado);
            Assert.Equal("Categoría eliminada correctamente.", result.SuccessMessage);
        }
    }
}
