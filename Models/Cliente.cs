using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public required string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public required string Apellido { get; set; }

        [DataType(DataType.Date)]
        public required DateTime FechaNacimiento { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Phone]
        [StringLength(15)]
        public required string Telefono { get; set; }

        [DataType(DataType.DateTime)]
        public required DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Relación uno a muchos con la tabla Pagos
        public ICollection<Pago>? Pagos { get; set; }
    }
}
