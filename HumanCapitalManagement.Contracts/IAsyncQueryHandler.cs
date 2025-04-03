namespace HumanCapitalManagement.Contracts
{
    public interface IAsyncQueryHandler<TQuery, TResult>
        where TQuery : class
        where TResult : class
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
