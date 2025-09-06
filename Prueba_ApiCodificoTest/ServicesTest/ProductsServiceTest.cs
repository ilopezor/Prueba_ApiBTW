using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Prueba_ApiCodificoTest.ServicesTest
{
    public class ProductsServiceTest
    {
        private readonly ProductsService _productsService;
        private readonly Mock<IRepository<Product>> _productsRepositoryMock;

        public ProductsServiceTest()
        {
            _productsRepositoryMock = new Mock<IRepository<Product>>();
            _productsService = new ProductsService(_productsRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProduct_ReturnsProducts()
        {
            var products = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "Prod1" },
                new Product { ProductId = 2, ProductName = "Prod2" }
            };
            _productsRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(products);

            var response = await _productsService.GetAllProduct();

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Equal(2, response.ObjectResponse.Count);
            Assert.Equal("Prod1", response.ObjectResponse[0].ProductName);
            Assert.Equal("Products retrieved successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }
    }
}