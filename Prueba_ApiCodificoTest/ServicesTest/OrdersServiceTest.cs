using Application.DTO;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Prueba_ApiCodificoTest.ServicesTest
{
    public class OrdersServiceTest
    {
        private readonly OrdersService _ordersService;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;

        public OrdersServiceTest()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _ordersService = new OrdersService(_orderRepositoryMock.Object);
        }

        [Fact]
        public async Task OrdersByCustomer_ReturnsOrders_WhenOrdersExist()
        {
            var customerId = 1;
            var orders = new List<Order>
            {
                new Order { Orderid = 1, Shipname = "Test" }
            };
            _orderRepositoryMock.Setup(r => r.GetOrderByCustomer(customerId))
                .ReturnsAsync(orders);

            var response = await _ordersService.OrdersByCustomer(customerId);

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Single(response.ObjectResponse);
            Assert.Equal(1, response.ObjectResponse[0].Orderid);
            Assert.Equal("Orders retrieved successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }

        [Fact]
        public async Task OrdersByCustomer_ThrowsException_WhenNoOrders()
        {
            var customerId = 2;
            _orderRepositoryMock.Setup(r => r.GetOrderByCustomer(customerId))
                .ReturnsAsync(new List<Order>());

            var response = await _ordersService.OrdersByCustomer(customerId);

            Assert.NotNull(response);
            Assert.False(response.OperationSuccess);
            Assert.Null(response.ObjectResponse);
            Assert.Equal("No orders found for the specified customer.", response.ErrorMessage);
        }

        [Fact]
        public async Task CreateOrder_ReturnsAddNewOrder()
        {
            var orderDto = new OrderDTO
            {
                employeeId = 1,
                customerId = 1,
                orderDate = DateTime.Now,
                requiredDate = DateTime.Now.AddDays(1),
                shipperId = 1,
                freight = 10,
                shipName = "Test",
                shipAddress = "Address",
                shipCity = "City",
                shipCountry = "Country",
                orderDetail = new OrderDetailDTO { discount = 10 }
            };
            var addNewOrder = new AddNewOrder { OrderId = 123 };
            _orderRepositoryMock.Setup(r => r.FromSqlRaw(It.IsAny<string>(), It.IsAny<Microsoft.Data.SqlClient.SqlParameter>()))
                .ReturnsAsync(addNewOrder);

            var response = await _ordersService.CreateOrder(orderDto);

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Equal(123, response.ObjectResponse.OrderId);
            Assert.Equal("Order created successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }
    }
}