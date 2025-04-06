using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestPharmacy1.Models.Medications
{
    public class DetailsMedicationViewModel
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(30)]
        [Required]
        public string Name { get; set; }
        [StringLength(40)]
        [Required]
        public string Manufacturer { get; set; }
        [AllowNull]
        [StringLength(300)]
        public string? HowToUse { get; set; }
        [Required]
        public bool IsPrescriptionNeeded { get; set; }
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
        public byte[] Image { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }
    }
}
