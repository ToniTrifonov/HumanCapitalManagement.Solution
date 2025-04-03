namespace HumanCapitalManagement.Contracts.Results.Roles
{
    public class AllRolesResult
    {
        public AllRolesResult(List<RoleItem> roles)
        {
            Roles = roles;
        }

        public List<RoleItem> Roles { get; }
    }
}
