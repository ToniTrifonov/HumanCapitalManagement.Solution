using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Handlers.Commands.Employees.Add;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Employees.Add
{
    [TestClass]
    public class AddEmployeeCommandHandlerTests
    {
        private readonly AddEmployeeCommandHandler handler;

        private readonly Mock<IEmployeesRepository> employeesRepositoryMock;
        private readonly Mock<IProjectsRepository> projectsRepositoryMock;

        public AddEmployeeCommandHandlerTests()
        {
            employeesRepositoryMock = new Mock<IEmployeesRepository>();
            projectsRepositoryMock = new Mock<IProjectsRepository>();

            handler = new AddEmployeeCommandHandler(employeesRepositoryMock.Object, projectsRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectMessage_WhenProjectDoesNotExist()
        {
            // Arrange
            projectsRepositoryMock
                .Setup(x => x.ProjectExists(It.IsAny<string>()))
                .ReturnsAsync(false);

            var command = new AddEmployeeCommand("test", "test", 123, EmployeePosition.FrontEndDeveloper, "test");

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(false, result.Succeed);
            Assert.AreEqual("Invalid project.", result.ErrorMessage);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnSuccessfulResult()
        {
            // Arrange
            projectsRepositoryMock
                .Setup(x => x.ProjectExists(It.IsAny<string>()))
                .ReturnsAsync(true);

            var command = new AddEmployeeCommand("test", "test", 123, EmployeePosition.FrontEndDeveloper, "test");

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(true, result.Succeed);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}
