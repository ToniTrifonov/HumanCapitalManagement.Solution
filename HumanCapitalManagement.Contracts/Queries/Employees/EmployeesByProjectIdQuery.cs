namespace HumanCapitalManagement.Contracts.Queries.Employees
{
    public class EmployeesByProjectIdQuery
    {
        public EmployeesByProjectIdQuery(string projectId)
        {
            ProjectId = projectId;
        }

        public string ProjectId { get; }
    }
}
