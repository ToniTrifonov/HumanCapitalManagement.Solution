using HumanCapitalManagement.Contracts.Queries.Projects;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Handlers.Queries.Projects;
using Moq;

namespace HumanCapitalManagement.Tests.Queries.Projects
{
    [TestClass]
    public class ProjectsByIdQueryHandlerTests
    {
        private readonly ProjectsByUserIdQueryHandler handler;
        private readonly Mock<IProjectsRepository> projectRepositoryMock;

        public ProjectsByIdQueryHandlerTests()
        {
            projectRepositoryMock = new Mock<IProjectsRepository>();
            handler = new ProjectsByUserIdQueryHandler(projectRepositoryMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnModelWithCorrectEmployeeData()
        {
            // Arrange
            var projects = new List<Project>()
            {
                new() { Id = "123", Size = 1000, Name = "testName", Description = "testDescription", CreateDate = DateTime.Now },
                new() { Id = "1234", Size = 1000, Name = "testName2", Description = "testDescription2", CreateDate = DateTime.Now },
            };

            projectRepositoryMock
                .Setup(x => x.ProjectsByUserId(It.IsAny<string>()))
                .ReturnsAsync(projects);

            var query = new ProjectsByUserIdQuery("test");

            // Act
            var result = await this.handler.HandleAsync(query);

            // Assert
            Assert.AreEqual(projects[0].Id, result.Projects[0].Id);
            Assert.AreEqual(projects[0].Size, result.Projects[0].Size);
            Assert.AreEqual(projects[0].Name, result.Projects[0].Name);
            Assert.AreEqual(projects[0].Description, result.Projects[0].Description);
            Assert.AreEqual(projects[0].CreateDate, result.Projects[0].CreateDate);
            Assert.AreNotEqual(projects[1].Name, result.Projects[0].Name);
        }
    }
}
