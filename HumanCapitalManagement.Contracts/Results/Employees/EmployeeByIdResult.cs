namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class EmployeeByIdResult
    {
        public EmployeeByIdResult(EmployeeResultItem employee)
        {
            Employee = employee;
        }

        public EmployeeResultItem Employee { get; }
    }
}
