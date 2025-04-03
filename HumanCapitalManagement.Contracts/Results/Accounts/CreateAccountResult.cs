namespace HumanCapitalManagement.Contracts.Results.Accounts
{
    public class CreateAccountResult
    {
        public CreateAccountResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
