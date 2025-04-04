namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class EmployeesByProjectIdResult
    {
        public EmployeesByProjectIdResult(List<EmployeeResultItem> employees)
        {
            Employees = employees;
        }

        public List<EmployeeResultItem> Employees { get; }
    }
}
