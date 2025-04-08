using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HumanCapitalManagement.Tests.Repositories
{
    [TestClass]
    public class AccountsRepositoryTests
    {
        private readonly AccountsRepository repository;
        private readonly Mock<ApplicationDbContext> contextMock;
        private readonly Mock<DbSet<IdentityUser>> usersMock;

        public AccountsRepositoryTests()
        {
            contextMock = new Mock<ApplicationDbContext>();
            usersMock = new Mock<DbSet<IdentityUser>>();
            contextMock.Setup(x => x.Set<IdentityUser>()).Returns(usersMock.Object);

            repository = new AccountsRepository(contextMock.Object);
        }

        [TestMethod]
        public async Task Add_ShouldCorrectlyAddEntityToCollection()
        {
            // Arrange
            var newUser = new IdentityUser()
            {
                Id = "123",
                UserName = "testUsername",
                Email = "test@test.com"
            };

            // Act
            await repository.Add(newUser);

            // Assert
            usersMock.Verify(x => x.AddAsync(newUser, default), Times.Once(), "AddAsync should be called once.");
            contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once(), "SaveChangesAsync should be called once.");
        }

        [TestMethod]
        public async Task UserEmailInUse_ShouldVerifyEmailAvailability()
        {
            // Arrange

            // Act
            var result = await repository.UserEmailInUse("test");

            // Assert
            usersMock.Verify(x => x.AnyAsync(default), Times.Once(), "AnyAsync should be called once.");
        }
    }
}
