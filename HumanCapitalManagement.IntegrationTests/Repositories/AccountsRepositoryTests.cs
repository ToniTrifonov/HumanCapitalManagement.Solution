using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.IntegrationTests.Repositories
{
    [TestClass]
    public class AccountsRepositoryTests
    {
        private readonly AccountsRepository repository;
        private readonly ApplicationDbContext context;

        public AccountsRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new ApplicationDbContext(contextOptions);
            repository = new AccountsRepository(context);
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
        public async Task Add_ShouldCorrectlyAddNewAccount()
        {
            // Arrange
            var newAccount = new IdentityUser()
            {
                Id = "123",
                Email = "test@test.com"
            };

            // Act
            await this.repository.Add(newAccount);
            var account = await this.context.Users.FindAsync(newAccount.Id);

            // Assert
            Assert.IsNotNull(account);
            Assert.AreEqual(newAccount.Id, account.Id);
            Assert.AreEqual(newAccount.Email, account.Email);
        }

        [TestMethod]
        public async Task UserEmailInUse_ShouldReturnTrue_WhenEmailInUse()
        {
            // Arrange
            var newAccount = new IdentityUser()
            {
                Id = "123",
                Email = "test@test.com"
            };
            this.context.Users.Add(newAccount);
            await this.context.SaveChangesAsync();

            // Act
            var inUse = await this.repository.UserEmailInUse(newAccount.Email);

            // Assert
            Assert.IsTrue(inUse);
        }

        [TestMethod]
        public async Task UserEmailInUse_ShouldReturnFalse_WhenEmailNotInUse()
        {
            // Arrange
            var newAccount = new IdentityUser()
            {
                Id = "123",
                Email = "test@test.com"
            };
            this.context.Users.Add(newAccount);
            await this.context.SaveChangesAsync();

            // Act
            var inUse = await this.repository.UserEmailInUse("testEmail@testEmail.com");

            // Assert
            Assert.IsFalse(inUse);
        }
    }
}
