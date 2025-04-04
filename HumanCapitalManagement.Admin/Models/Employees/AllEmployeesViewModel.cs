using HumanCapitalManagement.Contracts.Results.Employees;

namespace HumanCapitalManagement.Web.Models.Employees
{
    public class AllEmployeesViewModel
    {
        public AllEmployeesViewModel(List<EmployeeResultItem> employees)
        {
            Employees = employees;
        }

        public List<EmployeeResultItem> Employees { get; }
    }
}
