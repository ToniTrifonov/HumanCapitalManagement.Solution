using HumanCapitalManagement.Contracts.Results.Employees;

namespace HumanCapitalManagement.Web.Models.Employees
{
    public class EditEmployeeModel
    {
        public EditEmployeeModel(EmployeeResultItem employee)
        {
            Employee = employee;
        }

        public EmployeeResultItem Employee { get; }
    }
}
