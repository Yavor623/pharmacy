using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models.Precsriptions
{
    public class DeletePrescriptionViewModel
    {
        [Required]
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        [StringLength(200)]
        public string Medications { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly PrescribedDate { get; set; }
        [StringLength(150)]
        [Required]
        public string Description { get; set; }
    }
}
