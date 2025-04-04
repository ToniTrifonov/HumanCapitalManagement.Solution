namespace HumanCapitalManagement.Contracts.Results.Employees
{
    public class AddEmployeeResult
    {
        public AddEmployeeResult(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
