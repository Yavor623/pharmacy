using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Humanizer.Bytes;

namespace TestPharmacy1.Models.Medications
{
    public class CreateMedicationViewModel
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
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateOnly ExpirationDate { get; set; }
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
        public IFormFile ImageFile { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }
    }
}
