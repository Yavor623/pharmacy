using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Accounts
{
    public class CreateUserViewModel
    {
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
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Двете пароли не съвпадат!")]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
    }
}
