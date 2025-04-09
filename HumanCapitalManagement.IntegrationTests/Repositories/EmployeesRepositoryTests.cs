using HumanCapitalManagement.Data.Data;
using HumanCapitalManagement.Data.Entities;
using HumanCapitalManagement.Data.Enums;
using HumanCapitalManagement.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HumanCapitalManagement.IntegrationTests.Repositories
{
    [TestClass]
    public class EmployeesRepositoryTests
    {
        private readonly EmployeesRepository repository;
        private readonly ApplicationDbContext context;

        public EmployeesRepositoryTests()
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=.;Database=HumanCapitalManagementTests;TrustServerCertificate=true;Integrated Security=true")
                .Options;

            context = new ApplicationDbContext(contextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            repository = new EmployeesRepository(context);
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
            context.Dispose();
        }

        [TestMethod]
        public async Task Add_ShouldCorrectlyAddNewEmployee()
        {
            // Arrange
            var newEmployee = new Employee()
            {
                Id = "123",
                FirstName = "TestEmployee",
                LastName = "TestLastName",
            };

            // Act
            await this.repository.Add(newEmployee);
            var employee = await this.context.Employees.FindAsync(newEmployee.Id);

            // Assert
            Assert.IsNotNull(employee);
            Assert.AreEqual(employee.Id, newEmployee.Id);
            Assert.AreEqual(employee.FirstName, newEmployee.FirstName);
            Assert.AreEqual(employee.LastName, newEmployee.LastName);
        }

        [TestMethod]
        public async Task EmployeeById_ShouldReturnEmployee_WhenOneExists()
        {
            // Arrange
            var newEmployee = new Employee()
            {
                Id = "123",
                FirstName = "TestEmployee",
                LastName = "TestLastName",
            };
            this.context.Employees.Add(newEmployee);
            await this.context.SaveChangesAsync();

            // Act
            var employee = await this.repository.EmployeeById(newEmployee.Id);

            // Assert
            Assert.IsNotNull(employee);
            Assert.AreEqual(newEmployee.Id, employee.Id);
            Assert.AreEqual(newEmployee.FirstName, employee.FirstName);
            Assert.AreEqual(newEmployee.LastName, employee.LastName);
        }

        [TestMethod]
        public async Task EmployeeById_ShouldReturnNull_WhenOneDoesNotExist()
        {
            // Arrange

            // Act
            var employee = await this.repository.EmployeeById("123");

            // Assert
            Assert.IsNull(employee);
        }

        [TestMethod]
        public async Task EmployeesByProjectId_ShouldReturnNonDeletedProjectEmployees_WhenTheyExist()
        {
            // Arrange
            this.context.Users.Add(new IdentityUser() { Id = "1" });
            this.context.Projects.Add(new Project() { Id = "1", UserId = "1" });
            this.context.Projects.Add(new Project() { Id = "2", UserId = "1" });

            this.context.Employees.Add(new Employee() { Id = "123", IsDeleted = true, ProjectId = "1" });
            this.context.Employees.Add(new Employee() { Id = "1234", IsDeleted = false, ProjectId = "2" });
            this.context.Employees.Add(new Employee() { Id = "12345", IsDeleted = false, ProjectId = "1" });
            await this.context.SaveChangesAsync();

            // Act
            var employees = await this.repository.EmployeesByProjectId("1");

            // Assert
            Assert.IsTrue(employees.Count == 1);
            Assert.AreEqual("12345", employees[0].Id);
        }

        [TestMethod]
        public async Task EmployeesByProjectId_ShouldReturnEmptyCollection_WhenNoEmployeesFound()
        {
            // Arrange
            this.context.Users.Add(new IdentityUser() { Id = "1" });
            this.context.Projects.Add(new Project() { Id = "1", UserId = "1" });
            this.context.Projects.Add(new Project() { Id = "2", UserId = "1" });

            this.context.Employees.Add(new Employee() { Id = "123", IsDeleted = true, ProjectId = "1" });
            this.context.Employees.Add(new Employee() { Id = "1234", IsDeleted = false, ProjectId = "2" });
            this.context.Employees.Add(new Employee() { Id = "12345", IsDeleted = true, ProjectId = "2" });
            await this.context.SaveChangesAsync();

            // Act
            var employees = await this.repository.EmployeesByProjectId("1");

            // Assert
            Assert.IsTrue(employees.Count == 0);
        }

        [TestMethod]
        public async Task DeleteEmployee_ShouldUpdateIsDeletedProperty()
        {
            // Arrange
            this.context.Employees.Add(new Employee() { Id = "123", IsDeleted = false });
            await this.context.SaveChangesAsync();

            // Act
            await this.repository.DeleteEmployee("123");
            var deletedEmployee = await this.context.Employees.FindAsync("123");

            // Assert
            Assert.IsNotNull(deletedEmployee);
            Assert.AreEqual(true, deletedEmployee.IsDeleted);

        }

        [TestMethod]
        public async Task EditEmployee_ShouldUpdateEmployeeData()
        {
            // Arrange
            this.context.Employees.Add(new Employee() 
            { 
                Id = "123", 
                FirstName = "test", 
                LastName = "testLastName", 
                Salary = 123, 
                Position = EmployeePosition.QAEngineer
            });
            await this.context.SaveChangesAsync();

            // Act
            await this.repository.EditEmployee("123", "newName", "newLastName", 12345, EmployeePosition.BackEndDeveloper);
            var employee = await this.context.Employees.FindAsync("123");

            // Assert
            Assert.AreEqual("newName", employee.FirstName);
            Assert.AreEqual("newLastName", employee.LastName);
            Assert.AreEqual(12345, employee.Salary);
            Assert.AreEqual(EmployeePosition.BackEndDeveloper, employee.Position);
        }
    }
}
