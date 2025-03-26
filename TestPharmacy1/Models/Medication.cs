using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TestPharmacy1.Models
{
    public class Medication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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

        [ForeignKey("TypeOfMedicationId")]
        public int TypeOfMedicationId { get; set; }
        
        public TypeOfMedication TypeOfMedication { get; set; }

        [ForeignKey("ConsistencyOfMedicationId")]
        public int ConsistencyOfMedicationId { get; set; }
        public ConsistencyOfMedication ConsistencyOfMedication { get; set; }
        public OwnedMedication OwnedMedication { get; set; }
        [ForeignKey("ImageId")]
        public int ImageId { get; set; }
        public Image Image { get; set; }

    }
}
