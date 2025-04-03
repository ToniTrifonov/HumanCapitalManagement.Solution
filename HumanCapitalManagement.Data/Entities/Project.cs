using Microsoft.AspNetCore.Identity;

namespace HumanCapitalManagement.Data.Entities
{
    public class Project
    {
        public Project()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Size { get; set; }

        public DateTime CreateDate { get; set; }

        public string UserId { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
