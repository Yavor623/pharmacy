using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TestPharmacy1.Models.Medications
{
    public class CreateMedicationViewModel
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

        public int ConsistencyOfMedicationId { get; set; }
        public byte[] Picture { get; set; }
    }
}
