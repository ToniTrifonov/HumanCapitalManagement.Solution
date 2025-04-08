using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Employees;
using HumanCapitalManagement.Contracts.Results.Employees;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Handlers.Commands.Employees.Add;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Employees.Add
{
    [TestClass]
    public class AddEmployeeErrorHandlerTests
    {
        private readonly AddEmployeeErrorHandler errorHandler;

        private readonly Mock<IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>> addEmployeeMock;

        public AddEmployeeErrorHandlerTests()
        {
            addEmployeeMock = new Mock<IAsyncCommandHandler<AddEmployeeCommand, AddEmployeeResult>>();

            errorHandler = new AddEmployeeErrorHandler(addEmployeeMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            addEmployeeMock
                .Setup(x => x.HandleAsync(It.IsAny<AddEmployeeCommand>()))
                .ThrowsAsync(new Exception());

            var command = new AddEmployeeCommand("test", "test", 123, EmployeePosition.FrontEndDeveloper, "test");

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
            var expectedResult = new AddEmployeeResult();
            addEmployeeMock
                .Setup(x => x.HandleAsync(It.IsAny<AddEmployeeCommand>()))
                .ReturnsAsync(expectedResult);

            var command = new AddEmployeeCommand("test", "test", 123, EmployeePosition.FrontEndDeveloper, "test");

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
