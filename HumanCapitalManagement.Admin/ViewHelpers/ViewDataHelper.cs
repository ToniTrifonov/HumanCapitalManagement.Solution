using HumanCapitalManagement.Contracts.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanCapitalManagement.Web.ViewHelpers
{
    public static class ViewDataHelper
    {
        private static List<SelectListItem> allRoles;
        private static List<SelectListItem> employeePositions;

        public static List<SelectListItem> AllRoles()
        {
            if (allRoles == null)
            {
                allRoles = Enum.GetValues(typeof(UserRole)).Cast<UserRole>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = v.ToString()
                }).ToList();
            }

            return allRoles;
        }

        public static List<SelectListItem> AllEmployeePositions()
        {
            if (employeePositions == null)
            {
                employeePositions = Enum.GetValues(typeof(EmployeePosition)).Cast<EmployeePosition>().Select(v => new SelectListItem
                {
                    Text = v.ToString(),
                    Value = ((int)v).ToString()
                }).ToList();
            }

            return employeePositions;
        }
    }
}
