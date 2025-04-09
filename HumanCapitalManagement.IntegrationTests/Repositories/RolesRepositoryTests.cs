using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.IntegrationTests.Repositories
{
    [TestClass]
    public class RolesRepositoryTests
    {
        private readonly RolesRepository repository;
        private readonly ApplicationDbContext context;

        public RolesRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(contextOptions);
            repository = new RolesRepository(context);
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

        public async Task Add_ShouldCorrectlyAddNewRole()
        {
            // Arrange
            var newRole = new IdentityRole()
            {
                Id = "123",
                Name = "TestAdmin",
            };

            // Act
            await this.repository.Add(newRole);
            var role = await this.context.Projects.FindAsync(newRole.Id);

            // Assert
            Assert.IsNotNull(role);
            Assert.AreEqual(newRole.Id, role.Id);
            Assert.AreEqual(newRole.Name, role.Name);
        }

        [TestMethod]
        public async Task RoleIdByName_ShouldReturnRoleId_WhenRoleExists()
        {
            // Arrange
            var newRole = new IdentityRole()
            {
                Id = "123",
                Name = "TestAdmin",
            };
            this.context.Roles.Add(newRole);
            await this.context.SaveChangesAsync();

            // Act
            var roleId = await this.repository.RoleIdByName(newRole.Name);

            // Assert
            Assert.IsNotNull(roleId);
            Assert.AreEqual("123", roleId);
        }

        [TestMethod]
        public async Task RoleIdByName_ShouldReturnNull_WhenRoleDoesNotExists()
        {
            // Arrange
            var newRole = new IdentityRole()
            {
                Id = "123",
                Name = "TestAdmin",
            };
            this.context.Roles.Add(newRole);
            await this.context.SaveChangesAsync();

            // Act
            var roleId = await this.repository.RoleIdByName("TestProjectManagerRole");

            // Assert
            Assert.IsNull(roleId);
        }
    }
}
