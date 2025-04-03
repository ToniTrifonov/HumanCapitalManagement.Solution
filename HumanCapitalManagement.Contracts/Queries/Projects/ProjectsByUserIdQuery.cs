namespace HumanCapitalManagement.Contracts.Queries.Projects
{
    public class ProjectsByUserIdQuery
    {
        public ProjectsByUserIdQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}
