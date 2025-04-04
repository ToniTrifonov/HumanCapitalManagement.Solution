namespace HumanCapitalManagement.Contracts.Commands.Projects
{
    public class CreateProjectCommand
    {
        public CreateProjectCommand(string name, string description, int size, string userId)
        {
            Name = name;
            Description = description;
            Size = size;
            UserId = userId;
        }

        public string Name { get; }

        public string Description { get; }

        public int Size { get; }

        public string UserId { get; }
    }
}
