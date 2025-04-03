using HumanCapitalManagement.Contracts.Results.Projects;

namespace HumanCapitalManagement.Web.Models.Projects
{
    public class AllProjectsViewModel
    {
        public AllProjectsViewModel(List<ProjectsResultItem> projects)
        {
            Projects = projects;
        }

        public List<ProjectsResultItem> Projects { get; }
    }
}
