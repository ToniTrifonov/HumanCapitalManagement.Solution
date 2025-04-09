using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.IntegrationTests.Repositories
{
    [TestClass]
    public class ProjectsRepositoryTests
    {
        private readonly ProjectsRepository repository;
        private readonly ApplicationDbContext context;

        public ProjectsRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(contextOptions);
            repository = new ProjectsRepository(context);
        }

        [TestInitialize()]
        public void Startup()
        {
            context.Database.EnsureCreated();
        }

        [TestCleanup()]
        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        public async Task Add_ShouldCorrectlyAddNewProject()
        {
            // Arrange
            var newProject = new Project()
            {
                Id = "123",
                Name = "TestName",
            };

            // Act
            await this.repository.Add(newProject);
            var project = await this.context.Projects.FindAsync(newProject.Id);

            // Assert
            Assert.IsNotNull(project);
            Assert.AreEqual(project.Id, newProject.Id);
            Assert.AreEqual(project.Name, newProject.Name);
        }

        [TestMethod]
        public async Task ProjectExists_ShouldReturnTrue_WhenProjectExists()
        {
            // Arrange
            var newProject = new Project()
            {
                Id = "123",
                Name = "TestName",
            };
            this.context.Projects.Add(newProject);
            await this.context.SaveChangesAsync();

            // Act
            var exists = await this.repository.ProjectExists(newProject.Id);

            // Assert
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task ProjectExists_ShouldReturnFalse_WhenProjectDoesNotExists()
        {
            // Arrange
            var newProject = new Project()
            {
                Id = "123",
                Name = "TestName",
            };
            this.context.Projects.Add(newProject);
            await this.context.SaveChangesAsync();

            // Act
            var exists = await this.repository.ProjectExists("12345");

            // Assert
            Assert.IsFalse(exists);
        }

        [TestMethod]
        public async Task ProjectsByUserId_ShouldReturnAvailableProjects_WhenSuchExist()
        {
            // Arrange
            this.context.Users.Add(new IdentityUser() { Id = "1" });
            this.context.Users.Add(new IdentityUser() { Id = "2" });

            this.context.Projects.Add(new Project() { Id = "1", UserId = "1" });
            this.context.Projects.Add(new Project() { Id = "2", UserId = "1" });
            this.context.Projects.Add(new Project() { Id = "3", UserId = "2" });

            await this.context.SaveChangesAsync();

            // Act
            var projects = await this.repository.ProjectsByUserId("1");

            // Assert
            Assert.IsTrue(projects.Count == 2);
            Assert.IsTrue(!projects.Any(x => x.Id == "3"));
        }

        [TestMethod]
        public async Task ProjectsByUserId_ShouldReturnEmptyCollection_WhenNoProjectFound()
        {
            // Arrange
            this.context.Users.Add(new IdentityUser() { Id = "1" });
            this.context.Users.Add(new IdentityUser() { Id = "2" });

            this.context.Projects.Add(new Project() { Id = "1", UserId = "1" });
            this.context.Projects.Add(new Project() { Id = "2", UserId = "1" });
            this.context.Projects.Add(new Project() { Id = "3", UserId = "2" });

            await this.context.SaveChangesAsync();

            // Act
            var projects = await this.repository.ProjectsByUserId("3");

            // Assert
            Assert.IsTrue(projects.Count == 0);
        }
    }
}
