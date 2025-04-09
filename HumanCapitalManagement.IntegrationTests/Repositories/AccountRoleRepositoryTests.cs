using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.IntegrationTests.Repositories
{
    [TestClass]
    public class AccountRoleRepositoryTests
    {
        private readonly AccountRoleRepository repository;
        private readonly ApplicationDbContext context;

        public AccountRoleRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(contextOptions);
            repository = new AccountRoleRepository(context);
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

        [TestMethod]
        public async Task Add_ShouldCorrectlyAddNewAccountRole()
        {
            // Arrange
            this.context.Users.Add(new IdentityUser() { Id = "123" });
            this.context.Roles.Add(new IdentityRole() { Id = "123" });
            await this.context.SaveChangesAsync();
            var newUserRole = new IdentityUserRole<string>()
            {
                RoleId = "123",
                UserId = "123"
            };

            // Act
            await this.repository.Add(newUserRole);
            var userRole = await this.context.UserRoles.FindAsync(newUserRole.RoleId, newUserRole.UserId);

            // Assert
            Assert.IsNotNull(userRole);
            Assert.AreEqual("123", userRole.UserId);
            Assert.AreEqual("123", userRole.RoleId);
        }
    }
}
