using HumanCapitalManagement.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Web.Models.Employees
{
    public class AddEmployeeInputModel
    {
        [Required(ErrorMessage = "FirstName is required.", AllowEmptyStrings = false)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.", AllowEmptyStrings = false)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        public EmployeePosition Position { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive number.")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Project is required.")]
        public string ProjectId { get; set; }
    }
}
