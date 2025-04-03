namespace HumanCapitalManagement.Contracts.Results.Projects
{
    public class ProjectsByUserIdResult
    {
        public ProjectsByUserIdResult(List<ProjectsResultItem> projects)
        {
            Projects = projects;
        }

        public List<ProjectsResultItem> Projects { get; }
    }
}
