using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Prueba_ApiCodificoTest.ServicesTest
{
    public class CustomerServiceTest
    {
        private readonly CustomerService _customerService;
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public CustomerServiceTest()
        {
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerService = new CustomerService(_customerRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCustomerSalePrediction_ReturnsPredictions()
        {
            var predictions = new List<SalesDatePrediction>
            {
                new SalesDatePrediction { CustomerId = 1, CustomerName = "Cliente 1" },
                new SalesDatePrediction { CustomerId = 2, CustomerName = "Cliente 2" }
            };
            _customerRepositoryMock.Setup(r => r.FromSqlRaw(It.IsAny<string>()))
                .ReturnsAsync(predictions);

            var response = await _customerService.GetCustomerSalePrediction();

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Equal(2, response.ObjectResponse.Count);
            Assert.Equal("Cliente 1", response.ObjectResponse[0].CustomerName);
            Assert.Equal("Customer sales predictions retrieved successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }

        [Theory]
        [InlineData(1, "Cliente Uno", "2024-06-01", "2024-07-01")]
        [InlineData(2, "Cliente Dos", null, "2024-08-15")]
        [InlineData(3, "Cliente Tres", "2023-12-31", null)]
        public async Task GetCustomerSalePrediction_ReturnsCorrectData(
            int customerId,
            string customerName,
            string lastOrderDateStr,
            string nextPredictedOrderStr)
        {
            DateTime? lastOrderDate = string.IsNullOrEmpty(lastOrderDateStr) ? (DateTime?)null : DateTime.Parse(lastOrderDateStr);
            DateTime? nextPredictedOrder = string.IsNullOrEmpty(nextPredictedOrderStr) ? (DateTime?)null : DateTime.Parse(nextPredictedOrderStr);

            var predictions = new List<SalesDatePrediction>
            {
                new SalesDatePrediction
                {
                    CustomerId = customerId,
                    CustomerName = customerName,
                    LastOrderDate = lastOrderDate,
                    NextPredictedOrder = nextPredictedOrder
                }
            };
            _customerRepositoryMock.Setup(r => r.FromSqlRaw(It.IsAny<string>()))
                .ReturnsAsync(predictions);

            var response = await _customerService.GetCustomerSalePrediction();

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Single(response.ObjectResponse);
            Assert.Equal(customerId, response.ObjectResponse[0].CustomerId);
            Assert.Equal(customerName, response.ObjectResponse[0].CustomerName);
            Assert.Equal(lastOrderDate, response.ObjectResponse[0].LastOrderDate);
            Assert.Equal(nextPredictedOrder, response.ObjectResponse[0].NextPredictedOrder);
            Assert.Equal("Customer sales predictions retrieved successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }
    }
}