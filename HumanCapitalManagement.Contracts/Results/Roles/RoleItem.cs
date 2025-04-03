namespace HumanCapitalManagement.Contracts.Results.Roles
{
    public class RoleItem
    {
        public RoleItem(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }
    }
}
