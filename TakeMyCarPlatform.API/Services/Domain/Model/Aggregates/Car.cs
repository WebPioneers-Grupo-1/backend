using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeMyCar.Domain.Model.Aggregates
{
    [Table("cars")]
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; } 

        [Required]
        [MaxLength(255)]
        public string Url { get; set; }   

        [Required]
        public int Available { get; set; } = 1; // 1 = disponible, 0 = no disponible
    }
}
