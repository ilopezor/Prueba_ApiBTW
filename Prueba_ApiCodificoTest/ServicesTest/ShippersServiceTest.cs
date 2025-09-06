using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Prueba_ApiCodificoTest.ServicesTest
{
    public class ShippersServiceTest
    {
        private readonly ShippersService _shippersService;
        private readonly Mock<IRepository<Shipper>> _shippersRepositoryMock;

        public ShippersServiceTest()
        {
            _shippersRepositoryMock = new Mock<IRepository<Shipper>>();
            _shippersService = new ShippersService(_shippersRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllShippers_ReturnsShippers()
        {
            var shippers = new List<Shipper>
            {
                new Shipper { ShipperId = 1, CompanyName = "Shipper 1" },
                new Shipper { ShipperId = 2, CompanyName = "Shipper 2" }
            };
            _shippersRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(shippers);

            var response = await _shippersService.GetAllShippers();

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Equal(2, response.ObjectResponse.Count);
            Assert.Equal("Shipper 1", response.ObjectResponse[0].CompanyName);
            Assert.Equal("Shippers retrieved successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }
    }
}