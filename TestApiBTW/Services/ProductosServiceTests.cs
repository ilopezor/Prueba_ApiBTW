using Application.services;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Repository;
using Moq;
using System.Net;
using System.Text;

namespace TestApiBTW.Services
{
    public class ProductosServiceTests
    {
        private readonly Mock<IProductsRepository> _productosRepositoryMock;
        private readonly ProductosService _service;

        public ProductosServiceTests()
        {
            _productosRepositoryMock = new Mock<IProductsRepository>();
            _service = new ProductosService(_productosRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnProductos_WhenDataExists()
        {
            // Arrange
            var productos = new List<Productos>
            {
                new Productos
                {
                    IdProducto = 1,
                    NombreProducto = "Tetera",
                    Cantidad = 10,
                    Estado = true,
                    IdCategoria = 2,
                    PrecioUnitario = 2500,
                    IdCategoriaNavigation = new Categoria
                    {
                        IdCategoria = 2,
                        NombreCategoria = "Hogar"
                    }
                }
            };

            _productosRepositoryMock.Setup(r => r.GetAllWithIncludes())
                .ReturnsAsync(productos);

            // Act
            var result = await _service.GetAll();

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Single(result.ObjectResponse);
            Assert.Equal("Tetera", result.ObjectResponse[0].NombreProducto);
        }

        [Fact]
        public async Task SaveProduct_ShouldReturnError_WhenProductoIsNull()
        {
            // Act
            var result = await _service.SaveProduct(null);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal("Datos de producto inválidos.", result.ErrorMessage);
        }

        [Fact]
        public async Task SaveProduct_ShouldReturnError_WhenProductoExists()
        {
            // Arrange
            var productoDto = new ProductoDTO
            {
                NombreProducto = "Tetera",
                Estado = true
            };

            var productos = new List<Productos>
            {
                new Productos { NombreProducto = "Tetera", Estado = true }
            };

            _productosRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(productos);

            // Act
            var result = await _service.SaveProduct(productoDto);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal("El producto ya existe.", result.ErrorMessage);
        }

        [Fact]
        public async Task SaveProduct_ShouldSave_WhenProductoIsValid()
        {
            // Arrange
            var productoDto = new ProductoDTO
            {
                NombreProducto = "Tetera Nueva",
                Cantidad = 5,
                Estado = true,
                IdCategoria = 2,
                PrecioUnitario = 3000
            };

            _productosRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(new List<Productos>()); 

            _productosRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Productos>()))
                .ReturnsAsync(new Productos
                {
                    IdProducto = 1,
                    NombreProducto = "Tetera Nueva",
                    Cantidad = 5,
                    Estado = true,
                    IdCategoria = 2,
                    PrecioUnitario = 3000
                });

            // Act
            var result = await _service.SaveProduct(productoDto);

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Equal("Producto guardado correctamente.", result.SuccessMessage);
            Assert.Equal("Tetera Nueva", result.ObjectResponse.NombreProducto);
        }

        [Fact]
        public async Task UpdateProduct_ShouldReturnError_WhenProductoNotFound()
        {
            // Arrange
            var productoDto = new ProductoDTO { IdProducto = 99 };

            _productosRepositoryMock.Setup(r => r.GetByIdAsync(99))
                .ReturnsAsync((Productos)null);

            // Act
            var result = await _service.UpdateProduct(productoDto);

            // Assert
            Assert.False(result.OperationSuccess);
            Assert.Equal("Producto no encontrado.", result.ErrorMessage);
        }

        [Fact]
        public async Task DeleteProduct_ShouldUpdateEstado_WhenProductoExists()
        {
            // Arrange
            var producto = new Productos
            {
                IdProducto = 1,
                NombreProducto = "Cafetera",
                Estado = true
            };

            _productosRepositoryMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(producto);

            // Act
            var result = await _service.DeleteProduct(1);

            // Assert
            Assert.True(result.OperationSuccess);
            Assert.Equal("Estado del producto actualizado correctamente.", result.SuccessMessage);
            Assert.False(result.ObjectResponse.Estado);
        }
    }
}