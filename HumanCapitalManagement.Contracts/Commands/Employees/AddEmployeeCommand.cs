using HumanCapitalManagement.Data.Enums;

namespace HumanCapitalManagement.Contracts.Commands.Employees
{
    public class AddEmployeeCommand
    {
        public AddEmployeeCommand(string firstName, string lastName, decimal salary, EmployeePosition position, string projectId)
        {
            FirstName = firstName;
            LastName = lastName;
            Salary = salary;
            Position = position;
            ProjectId = projectId;
        }

        public string FirstName { get; }

        public string LastName { get; }

        public decimal Salary { get; }

        public EmployeePosition Position { get; }

        public string ProjectId { get; }
    }
}
