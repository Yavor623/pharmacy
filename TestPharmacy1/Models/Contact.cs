using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models
{
    public class Contact
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
