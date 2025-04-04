using HumanCapitalManagement.Contracts.Results.Employees;

namespace HumanCapitalManagement.Web.Models.Employees
{
    public class AllEmployeesViewModel
    {
        public AllEmployeesViewModel(List<EmployeeResultItem> employees, string projectId)
        {
            Employees = employees;
            ProjectId = projectId;
        }

        public List<EmployeeResultItem> Employees { get; }

        public string ProjectId { get; }
    }
}
