using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HumanCapitalManagement.UnitTests.Repositories
{
    [TestClass]
    public class ProjectsRepositoryTests
    {
        private readonly ProjectsRepository repository;
        private readonly Mock<ApplicationDbContext> contextMock;
        private readonly Mock<DbSet<Project>> projectsMock;

        public ProjectsRepositoryTests()
        {
            contextMock = new Mock<ApplicationDbContext>();
            projectsMock = new Mock<DbSet<Project>>();
            contextMock.Setup(x => x.Set<Project>()).Returns(projectsMock.Object);

            repository = new ProjectsRepository(contextMock.Object);
        }

        [TestMethod]
        public async Task Add_ShouldCorrectlyAddEntityToCollection()
        {
            // Arrange
            var newProject = new Project()
            {
                Id = "123",
                Name = "testProject",
                Description = "test description"
            };

            // Act
            await repository.Add(newProject);

            // Assert
            projectsMock.Verify(x => x.AddAsync(newProject, default), Times.Once(), "AddAsync should be called once.");
            contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once(), "SaveChangesAsync should be called once.");
        }

        [TestMethod]
        public async Task ProjectExists_ShouldVerifyProjectExists()
        {
            // Arrange

            // Act
            var result = await repository.ProjectExists("test");

            // Assert
            projectsMock.Verify(x => x.AnyAsync(default), Times.Once(), "AnyAsync should be called once.");
        }
    }
}
