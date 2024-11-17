using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Models
{
    public class Membresia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Tipo { get; set; }

        [StringLength(255)]
        public required string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public required decimal Precio { get; set; }

        [Required]
        public required int DuracionDias { get; set; }

        // Relación uno a muchos con la tabla Pagos
        public ICollection<Pago>? Pagos { get; set; }
    }
}
