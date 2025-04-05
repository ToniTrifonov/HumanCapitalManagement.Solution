namespace HumanCapitalManagement.Contracts.Commands.Employees
{
    public class DeleteEmployeeCommand
    {
        public DeleteEmployeeCommand(string id)
        {
            Id = id;
        }

        public string Id { get; }
    }
}
