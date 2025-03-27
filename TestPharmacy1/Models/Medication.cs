using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TestPharmacy1.Models
{
    public class Medication
    {
        [Key]
        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; }
        [StringLength(40)]
        public string Manufacturer { get; set; }
        [DataType(DataType.Date)]
        public DateOnly ExpirationDate { get; set; }
        public bool IsPrescriptionNeeded { get; set; }
        public int Amount { get; set; }
        [StringLength(150)]
        [AllowNull]
        public string Description { get; set; }
        public int TypeOfMedicationId { get; set; }
        [ValidateNever]
        [ForeignKey("TypeOfMedicationId")]

        public TypeOfMedication TypeOfMedication { get; set; }
        
        public int ConsistencyOfMedicationId { get; set; }
        [ValidateNever]
        [ForeignKey("ConsistencyOfMedicationId")]
        public ConsistencyOfMedication ConsistencyOfMedication { get; set; }
        public OwnedMedication OwnedMedication { get; set; }
        [ForeignKey("ImageId")]
        public byte[] Image { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Price { get; set; }

    }
}
