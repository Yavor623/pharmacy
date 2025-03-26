using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestPharmacy1.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(40)]
        public string Name { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public Medication Medication { get; set; }
    }
}
