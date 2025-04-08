using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Handlers.Commands.Employees.Edit;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Employees.Edit
{
    [TestClass]
    public class EditEmployeeCommandHandlerTests
    {
        private readonly EditEmployeeCommandHandler handler;
        private readonly Mock<IEmployeesRepository> employeesRepositoryMock;

        public EditEmployeeCommandHandlerTests()
        {
            employeesRepositoryMock = new Mock<IEmployeesRepository>();
            handler = new EditEmployeeCommandHandler(employeesRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenEmployeeDoesNotExist()
        {
            // Arrange
            employeesRepositoryMock
                .Setup(x => x.EmployeeById(It.IsAny<string>()))
                .ReturnsAsync((Employee?)null);

            var command = new EditEmployeeCommand("test", "test", "test", EmployeePosition.BackEndDeveloper, 123);

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.AreEqual("Employee does not exist.", result.ErrorMessage);
            Assert.AreEqual(false, result.Succeed);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnSuccessfulResult_WhenEmployeeExists()
        {
            // Arrange
            employeesRepositoryMock
                .Setup(x => x.EmployeeById(It.IsAny<string>()))
                .ReturnsAsync(new Employee());

            var command = new EditEmployeeCommand("test", "test", "test", EmployeePosition.BackEndDeveloper, 123);

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(true, result.Succeed);
        }
    }
}
