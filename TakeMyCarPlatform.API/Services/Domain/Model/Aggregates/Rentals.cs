using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeMyCar.Domain.Model.Aggregates
{
    [Table("rentals")]
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required]
        public DateTime StartDate { get; set; } 

        [Required]
        public DateTime EndDate { get; set; }  

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; } 

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        // Opciones adicionales
        public bool Gps { get; set; } = false;
        public bool Insurance { get; set; } = false;
        public bool ChildSeat { get; set; } = false;

        // Relación con la entidad Car
        public Car Car { get; set; }
    }
}
