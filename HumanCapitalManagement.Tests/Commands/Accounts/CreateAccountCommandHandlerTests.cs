using HumanCapitalManagement.Contracts;
using HumanCapitalManagement.Contracts.Commands.Accounts;
using HumanCapitalManagement.Contracts.Queries.Passwords;
using HumanCapitalManagement.Contracts.Results.Passwords;
using HumanCapitalManagement.Data.Contracts;
using HumanCapitalManagement.Handlers.Commands.Accounts;
using Moq;

namespace HumanCapitalManagement.Tests.Commands.Accounts
{
    [TestClass]
    public class CreateAccountCommandHandlerTests
    {
        private readonly CreateAccountCommandHandler createAccountHandler;

        private readonly Mock<IAccountsRepository> accountsRepositoryMock;
        private readonly Mock<IRolesRepository> rolesRepositoryMock;
        private readonly Mock<IAccountRoleRepository> accountsRolesRepositoryMock;
        private readonly Mock<IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult>> passwordHasherMock;

        public CreateAccountCommandHandlerTests()
        {
            accountsRepositoryMock = new Mock<IAccountsRepository>();
            rolesRepositoryMock = new Mock<IRolesRepository>();
            accountsRolesRepositoryMock = new Mock<IAccountRoleRepository>();
            passwordHasherMock = new Mock<IAsyncQueryHandler<GetHashedPasswordQuery, GetHashedPasswordResult>>();

            createAccountHandler = new CreateAccountCommandHandler(
                accountsRepositoryMock.Object, rolesRepositoryMock.Object, accountsRolesRepositoryMock.Object, passwordHasherMock.Object);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenEmailIsInUse()
        {
            // Arrange
            accountsRepositoryMock
                .Setup(x => x.UserEmailInUse(It.IsAny<string>()))
                .ReturnsAsync(true);

            var command = new CreateAccountCommand("test", "testPassword", "testRole");

            // Act
            var result = await this.createAccountHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(false, result.Succeed);
            Assert.AreEqual("Email already in use.", result.Message);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnFailedResultWithCorrectErrorMessage_WhenRoleDoesNotExist()
        {
            // Arrange
            accountsRepositoryMock
                .Setup(x => x.UserEmailInUse(It.IsAny<string>()))
                .ReturnsAsync(false);
            rolesRepositoryMock
                .Setup(x => x.RoleIdByName(It.IsAny<string>()))
                .ReturnsAsync((string?)null);

            var command = new CreateAccountCommand("test", "testPassword", "testRole");

            // Act
            var result = await this.createAccountHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(false, result.Succeed);
            Assert.AreEqual("Role does not exist.", result.Message);
        }

        [TestMethod]
        public async Task HandleAsync_ShouldReturnSuccessfulResult()
        {
            // Arrange
            accountsRepositoryMock
                .Setup(x => x.UserEmailInUse(It.IsAny<string>()))
                .ReturnsAsync(false);
            rolesRepositoryMock
                .Setup(x => x.RoleIdByName(It.IsAny<string>()))
                .ReturnsAsync("123");
            passwordHasherMock
                .Setup(x => x.HandleAsync(It.IsAny<GetHashedPasswordQuery>()))
                .ReturnsAsync(new GetHashedPasswordResult("hashedPassTest"));

            var command = new CreateAccountCommand("test", "testPassword", "testRole");

            // Act
            var result = await this.createAccountHandler.HandleAsync(command);

            // Assert
            Assert.AreEqual(true, result.Succeed);
            Assert.AreEqual("Account successfully created.", result.Message);
        }
    }
}
