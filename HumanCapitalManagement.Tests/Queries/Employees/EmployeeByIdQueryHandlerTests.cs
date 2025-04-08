using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Handlers.Queries.Employees;
using Moq;

namespace HumanCapitalManagement.Tests.Queries.Employees
{
    [TestClass]
    public class EmployeeByIdQueryHandlerTests
    {
        private readonly EmployeeByIdQueryHandler handler;
        private readonly Mock<IEmployeesRepository> employeesRepositoryMock;

        public EmployeeByIdQueryHandlerTests()
        {
            employeesRepositoryMock = new Mock<IEmployeesRepository>();
            handler = new EmployeeByIdQueryHandler(employeesRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnModelWithCorrectEmployeeData()
        {
            // Arrange
            var employee = new Employee()
            {
                Id = "123",
                FirstName = "testFirst",
                LastName = "testLast",
                Salary = 123,
                Position = EmployeePosition.BackEndDeveloper
            };

            employeesRepositoryMock
                .Setup(x => x.EmployeeById(It.IsAny<string>()))
                .ReturnsAsync(employee);

            var query = new EmployeeByIdQuery("testId");

            // Act
            var result = await this.handler.HandleAsync(query);

            // Assert
            Assert.AreEqual(employee.Id, result.Employee.Id);
            Assert.AreEqual(employee.FirstName, result.Employee.FirstName);
            Assert.AreEqual(employee.LastName, result.Employee.LastName);
            Assert.AreEqual(employee.Position, result.Employee.Position);
            Assert.AreEqual(employee.Salary, result.Employee.Salary);
        }
    }
}
