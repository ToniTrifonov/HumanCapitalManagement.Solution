using HumanCapitalManagement.Data.Entities;

namespace HumanCapitalManagement.Data.Contracts
{
    public interface IProjectsRepository : IApplicationRepository<Project>
    {
        Task<List<Project>> ProjectsByUserId(string userId);

        Task<bool> ProjectExists(string projectId);
    }
}
