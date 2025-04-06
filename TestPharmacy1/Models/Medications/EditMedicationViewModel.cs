using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestPharmacy1.Models.Medications
{
    public class EditMedicationViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(40)]
        public string Manufacturer { get; set; }
        [AllowNull]
        [StringLength(300)]
        public string? HowToUse { get; set; }
        [Required]
        public bool IsPrescriptionNeeded { get; set; }
        [Required]
        public int Amount { get; set; }
        [StringLength(150)]
        [AllowNull]
        public string Description { get; set; }
        [Required]

        public int TypeOfMedicationId { get; set; }
        [Required]
        public int ConsistencyOfMedicationId { get; set; }
        [Required]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Required]
        public decimal Price { get; set; }
        public Medication CurrentMedication { get; set; }
    }
}
