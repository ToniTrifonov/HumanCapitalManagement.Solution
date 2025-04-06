namespace HumanCapitalManagement.Contracts.Results.Projects
{
    public class CreateProjectResult
    {
        public CreateProjectResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Succeed = false;
        }

        public CreateProjectResult()
        {
            Succeed = true;
        }

        public string ErrorMessage { get; }

        public bool Succeed { get; }
    }
}
