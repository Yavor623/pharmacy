using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models
{
    public class EditInformationViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string AboutUs { get; set; }
    }
}
