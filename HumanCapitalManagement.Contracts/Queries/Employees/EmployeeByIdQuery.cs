namespace HumanCapitalManagement.Contracts.Queries.Employees
{
    public class EmployeeByIdQuery
    {
        public EmployeeByIdQuery(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
