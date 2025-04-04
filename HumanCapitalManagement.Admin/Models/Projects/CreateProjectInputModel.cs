using System.ComponentModel.DataAnnotations;

namespace HumanCapitalManagement.Web.Models.Projects
{
    public class CreateProjectInputModel
    {
        [Required(ErrorMessage = "Name is required.", AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required.", AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Size is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Size must be a positive number.")]
        public int Size { get; set; }
    }
}
