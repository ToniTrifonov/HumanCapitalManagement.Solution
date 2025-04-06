using HumanCapitalManagement.Contracts.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanCapitalManagement.Web.ViewHelpers
{
    public static class ViewDataHelper
    {
        private readonly static List<SelectListItem> allRoles;
        private readonly static List<SelectListItem> employeePositions;

        static ViewDataHelper()
        {
            allRoles = Enum.GetValues<UserRole>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = v.ToString()
            }).ToList();

            employeePositions = Enum.GetValues<EmployeePosition>().Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList();
        }

        public static List<SelectListItem> AllRoles => allRoles;

        public static List<SelectListItem> AllEmployeePositions => employeePositions;
    }
}
