using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Contacts
{
    public class EditContactsViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
