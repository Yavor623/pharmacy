using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Roles
{
	public class CreateRoleViewModel
	{
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
