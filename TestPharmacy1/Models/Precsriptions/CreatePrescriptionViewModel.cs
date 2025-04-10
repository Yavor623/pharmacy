using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Precsription
{
    public class CreatePrescriptionViewModel
    {
        [Key]
        public int PrescriptionId { get; set; }
        [StringLength(200)]
        public string Medications { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly PrescribedDate { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
    }
}
