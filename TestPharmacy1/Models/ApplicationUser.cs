using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(20)]
        public string FirstName { get; set; }
        [StringLength(20)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public OwnedMedication OwnedMedication { get; set; }
    }
}
