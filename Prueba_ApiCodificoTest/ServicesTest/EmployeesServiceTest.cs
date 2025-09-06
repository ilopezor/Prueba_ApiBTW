using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Prueba_ApiCodificoTest.ServicesTest
{
    public class EmployeesServiceTest
    {
        private readonly EmployeesService _employeesService;
        private readonly Mock<IRepository<Employee>> _employeeRepositoryMock;

        public EmployeesServiceTest()
        {
            _employeeRepositoryMock = new Mock<IRepository<Employee>>();
            _employeesService = new EmployeesService(_employeeRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnsEmployeeDTOs()
        {
            var employees = new List<Employee>
            {
                new Employee { EmpId = 1, FirstName = "John", LastName = "Doe" },
                new Employee { EmpId = 2, FirstName = "Jane", LastName = "Smith" }
            };
            _employeeRepositoryMock.Setup(r => r.GetAllAsync())
                .ReturnsAsync(employees);

            var response = await _employeesService.GetAllEmployees();

            Assert.NotNull(response);
            Assert.True(response.OperationSuccess);
            Assert.NotNull(response.ObjectResponse);
            Assert.Equal(2, response.ObjectResponse.Count);
            Assert.Equal("John Doe", response.ObjectResponse[0].FullName);
            Assert.Equal("Employees retrieved successfully.", response.SuccessMessage);
            Assert.Null(response.ErrorMessage);
        }
    }
}