using HumanCapitalManagement.Contracts.Enums;

namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class EmployeeResultItem
    {
        public EmployeeResultItem(string id, string firstName, string lastName, EmployeePosition position, decimal salary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            Salary = salary;
        }

        public string Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public EmployeePosition Position { get; }

        public decimal Salary { get; }
    }
}
