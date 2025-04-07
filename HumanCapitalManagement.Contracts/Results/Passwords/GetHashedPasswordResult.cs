namespace HumanCapitalManagement.Contracts.Results.Passwords
{
    public class GetHashedPasswordResult
    {
        public GetHashedPasswordResult(string hashedPassword)
        {
            HashedPassword = hashedPassword;
        }

        public string HashedPassword { get; }
    }
}
