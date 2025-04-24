using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Roles
{
	public class EditRoleViewModel
	{
		[Required]
		public string Id { get; set; }
		[Required]
		public string Name { get; set; }
        public IdentityRole Role { get; set; }
    }
}
