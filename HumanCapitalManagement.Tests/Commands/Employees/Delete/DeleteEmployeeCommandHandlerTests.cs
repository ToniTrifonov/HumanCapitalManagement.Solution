using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Handlers.Commands.Employees.Delete;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Employees.Delete
{
    [TestClass]
    public class DeleteEmployeeCommandHandlerTests
    {
        private readonly DeleteEmployeeCommandHandler handler;

        private readonly Mock<IEmployeesRepository> employeesRepositoryMock;

        public DeleteEmployeeCommandHandlerTests()
        {
            employeesRepositoryMock = new Mock<IEmployeesRepository>();

            handler = new DeleteEmployeeCommandHandler(employeesRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenEmployeeDoesNotExist()
        {
            // Arrange
            employeesRepositoryMock
                .Setup(x => x.EmployeeById(It.IsAny<string>()))
                .ReturnsAsync((Employee?)null);

            var command = new DeleteEmployeeCommand("test");

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.AreEqual("An employee with that id does not exist.", result.ErrorMessage);
            Assert.AreEqual(false, result.Succeed);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnSuccessfulResult_WhenEmployeeExists()
        {
            // Arrange
            employeesRepositoryMock
                .Setup(x => x.EmployeeById(It.IsAny<string>()))
                .ReturnsAsync(new Employee());

            var command = new DeleteEmployeeCommand("test");

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.IsNull(result.ErrorMessage);
            Assert.AreEqual(true, result.Succeed);
        }
    }
}
