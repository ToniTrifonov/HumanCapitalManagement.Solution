using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Handlers.Commands.Employees.Edit;
using Moq;

namespace HumanCapitalManagement.UnitTests.Commands.Employees.Edit
{
    [TestClass]
    public class EditEmployeeErrorHandlerTests
    {
        private readonly EditEmployeeErrorHandler errorHandler;
        private readonly Mock<IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>> editEmployeeMock;

        public EditEmployeeErrorHandlerTests()
        {
            editEmployeeMock = new Mock<IAsyncCommandHandler<EditEmployeeCommand, EditEmployeeResult>>();
            errorHandler = new EditEmployeeErrorHandler(editEmployeeMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            editEmployeeMock
                .Setup(x => x.HandleAsync(It.IsAny<EditEmployeeCommand>()))
                .ThrowsAsync(new Exception());

            var command = new EditEmployeeCommand("test", "test", "test", EmployeePosition.BackEndDeveloper, 123);

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(false, result.Succeed);
            Assert.AreEqual("An unexpected error occurred.", result.ErrorMessage);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnTheResultOfTheHandlerItDecorates_WhenNoException()
        {
            // Arrange
            var expectedResult = new EditEmployeeResult();
            editEmployeeMock
                .Setup(x => x.HandleAsync(It.IsAny<EditEmployeeCommand>()))
                .ReturnsAsync(expectedResult);

            var command = new EditEmployeeCommand("test", "test", "test", EmployeePosition.BackEndDeveloper, 123);

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
