using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPharmacy1.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        [StringLength(200)]
        public string Medications { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly PrescribedDate { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}
