using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Handlers.Commands.Employees.Delete;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Employees.Delete
{
    [TestClass]
    public class DeleteEmployeeErrorHandlerTests
    {
        private readonly DeleteEmployeeErrorHandler errorHandler;

        private readonly Mock<IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>> deleteEmployeeMock;

        public DeleteEmployeeErrorHandlerTests()
        {
            deleteEmployeeMock = new Mock<IAsyncCommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>>();

            errorHandler = new DeleteEmployeeErrorHandler(deleteEmployeeMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            deleteEmployeeMock
                .Setup(x => x.HandleAsync(It.IsAny<DeleteEmployeeCommand>()))
                .ThrowsAsync(new Exception());

            var command = new DeleteEmployeeCommand("test");

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
            var expectedResult = new DeleteEmployeeResult();
            deleteEmployeeMock
                .Setup(x => x.HandleAsync(It.IsAny<DeleteEmployeeCommand>()))
                .ReturnsAsync(expectedResult);

            var command = new DeleteEmployeeCommand("test");

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
