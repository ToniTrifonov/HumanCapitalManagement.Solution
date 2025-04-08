using HumanCapitalManagement.Contracts.Commands.Projects;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Handlers.Commands.Projects;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Projects
{
    [TestClass]
    public class CreateProjectCommandHandlerTests
    {
        private readonly CreateProjectCommandHandler handler;
        private readonly Mock<IProjectsRepository> projectsRepositoryMock;

        public CreateProjectCommandHandlerTests()
        {
            projectsRepositoryMock = new Mock<IProjectsRepository>();
            handler = new CreateProjectCommandHandler(projectsRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnSuccessfulResult()
        {
            // Arrange
            var command = new CreateProjectCommand("test", "test", 1000, "test");

            // Act
            var result = await this.handler.HandleAsync(command);

            // Assert
            Assert.AreEqual(true, result.Succeed);
            Assert.IsNull(result.ErrorMessage);
        }
    }
}
