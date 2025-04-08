using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Results.Accounts;
using HumanCapitalManagement.Handlers.Commands.Accounts;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Accounts
{
    [TestClass]
    public class CreateAccountErrorHandlerTests
    {
        private readonly CreateAccountErrorHandler errorHandler;

        private readonly Mock<IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>> mockObject;

        public CreateAccountErrorHandlerTests()
        {
            mockObject = new Mock<IAsyncCommandHandler<CreateAccountCommand, CreateAccountResult>>();

            errorHandler = new CreateAccountErrorHandler(mockObject.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnSuccessfulResult()
        {
            // Arrange
            var expectedResult = new CreateAccountResult("Successful result", succeed: true);
            mockObject.Setup(x => x.HandleAsync(It.IsAny<CreateAccountCommand>())).ReturnsAsync(expectedResult);

            var command = new CreateAccountCommand("testEmail", "testPass", "testRole");

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(expectedResult.Succeed, result.Succeed);
            Assert.AreEqual(expectedResult.Message, result.Message);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectMessage_WhenExceptionIsThrown()
        {
            // Arrange
            mockObject.Setup(x => x.HandleAsync(It.IsAny<CreateAccountCommand>())).ThrowsAsync(new Exception("test"));

            var command = new CreateAccountCommand("testEmail", "testPass", "testRole");
            var expectedResult = new CreateAccountResult("An unexpected error occurred.", succeed: false);

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(expectedResult.Succeed, result.Succeed);
            Assert.AreEqual(expectedResult.Message, result.Message);
        }
    }
}
