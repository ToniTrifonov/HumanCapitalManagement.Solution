namespace HumanCapitalManagement.Data.Contracts
{
    public interface IApplicationRepository<T>
        where T : class
    {
        Task Add(T entity);
    }
}
