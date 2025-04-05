namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class DeleteEmployeeResult
    {
        public DeleteEmployeeResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Succeed = false;
        }

        public DeleteEmployeeResult()
        {
            Succeed = true;
        }

        public string ErrorMessage { get; }

        public bool Succeed { get; }
    }
}
