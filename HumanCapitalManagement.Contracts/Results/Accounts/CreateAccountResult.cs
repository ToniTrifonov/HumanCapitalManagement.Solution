namespace HumanCapitalManagement.Contracts.Results.Accounts
{
    public class CreateAccountResult
    {
        public CreateAccountResult(string message, bool succeed)
        {
            Message = message;
            Succeed = succeed;
        }

        public string Message { get; }

        public bool Succeed { get; }
    }
}
