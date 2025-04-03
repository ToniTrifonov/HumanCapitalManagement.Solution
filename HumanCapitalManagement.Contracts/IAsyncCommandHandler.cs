namespace HumanCapitalManagement.Contracts
{
    public interface IAsyncCommandHandler<TCommand, TResult>
        where TCommand : class
        where TResult : class
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
