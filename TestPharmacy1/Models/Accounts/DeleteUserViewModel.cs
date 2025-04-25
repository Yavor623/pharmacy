using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Accounts
{
    public class DeleteUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Family Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Age { get; set; }
    }
}
