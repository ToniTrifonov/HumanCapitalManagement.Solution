using HumanCapitalManagement.Contracts.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HumanCapitalManagement.Web.ViewHelpers
{
    public static class ViewDataHelper
    {
        private static List<SelectListItem> allRoles;

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
    }
}
