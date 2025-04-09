using HumanCapitalManagement.Contracts.Queries.Employees;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Handlers.Queries.Employees;
using Moq;

namespace HumanCapitalManagement.UnitTests.Queries.Employees
{
    [TestClass]
    public class EmployeesByProjectIdQueryHandlerTests
    {
        private readonly EmployeesByProjectIdQueryHandler handler;
        private readonly Mock<IEmployeesRepository> employeesRepositoryMock;

        public EmployeesByProjectIdQueryHandlerTests()
        {
            employeesRepositoryMock = new Mock<IEmployeesRepository>();
            handler = new EmployeesByProjectIdQueryHandler(employeesRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnModelWithCorrectEmployeeData()
        {
            // Arrange
            var employees = new List<Employee>()
            {
                new() { Id = "123", FirstName = "testFirst", LastName = "testLast", Salary = 123, Position = EmployeePosition.BackEndDeveloper },
                new() { Id = "1234", FirstName = "testFirst2", LastName = "testLast2", Salary = 1234, Position = EmployeePosition.FrontEndDeveloper }
            };

            employeesRepositoryMock
                .Setup(x => x.EmployeesByProjectId(It.IsAny<string>()))
                .ReturnsAsync(employees);

            var query = new EmployeesByProjectIdQuery("testId");

            // Act
            var result = await this.handler.HandleAsync(query);

            // Assert
            Assert.AreEqual(employees[1].Id, result.Employees[1].Id);
            Assert.AreEqual(employees[1].FirstName, result.Employees[1].FirstName);
            Assert.AreEqual(employees[1].LastName, result.Employees[1].LastName);
            Assert.AreEqual(employees[1].Position, result.Employees[1].Position);
            Assert.AreEqual(employees[1].Salary, result.Employees[1].Salary);
            Assert.AreNotEqual(employees[0].FirstName, result.Employees[1].FirstName);
        }
    }
}
