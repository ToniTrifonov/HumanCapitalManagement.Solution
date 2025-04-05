namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class EditEmployeeResult
    {
        public EditEmployeeResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            Succeed = false;
        }
        public EditEmployeeResult()
        {
            Succeed = true;
        }

        public string ErrorMessage { get; }

        public bool Succeed { get; }
    }
}
