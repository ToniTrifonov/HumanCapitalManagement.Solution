namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class AddEmployeeResult
    {
        public AddEmployeeResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Succeed = false;
        }

        public AddEmployeeResult()
        {
            Succeed = true;
        }

        public string ErrorMessage { get; }

        public bool Succeed { get; }
    }
}
