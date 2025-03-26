using System.ComponentModel.DataAnnotations;

namespace TestPharmacy1.Models
{
    public class ConsistencyOfMedication
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public Medication Medication { get; set; }
    }
}
