using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TestPharmacy1.Models
{
    public class TypeOfMedication
    {
        [Key]
        public int Id { get; set; }
        [StringLength(80)]
        public string Name { get; set; }
        public Medication Medication { get; set; }
    }
}
