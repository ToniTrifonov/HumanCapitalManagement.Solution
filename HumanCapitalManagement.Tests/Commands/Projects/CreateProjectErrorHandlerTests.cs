using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Contracts.Results.Projects;
using HumanCapitalManagement.Handlers.Commands.Projects;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Projects
{
    [TestClass]
    public class CreateProjectErrorHandlerTests
    {
        private readonly CreateProjectErrorHandler errorHandler;

        private readonly Mock<IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>> createProjectMock;

        public CreateProjectErrorHandlerTests()
        {
            createProjectMock = new Mock<IAsyncCommandHandler<CreateProjectCommand, CreateProjectResult>>();

            errorHandler = new CreateProjectErrorHandler(createProjectMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenExceptionIsThrown()
        {
            // Arrange
            createProjectMock
                .Setup(x => x.HandleAsync(It.IsAny<CreateProjectCommand>()))
                .ThrowsAsync(new Exception());

            var command = new CreateProjectCommand("test", "test", 123, "test");

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
            var expectedResult = new CreateProjectResult();
            createProjectMock
                .Setup(x => x.HandleAsync(It.IsAny<CreateProjectCommand>()))
                .ReturnsAsync(expectedResult);

            var command = new CreateProjectCommand("test", "test", 123, "test");

            // Act
            var result = await this.errorHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }
    }
}
