using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AccesoDatos.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClienteID { get; set; }

        // Objeto de navegación
        [ForeignKey("ClienteID")]
        public Cliente? Cliente { get; set; }

        [Required]
        public int MembresiaID { get; set; }

        // Objeto de navegación
        [ForeignKey("MembresiaID")]
        public Membresia? Membresia { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Monto { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaPago { get; set; } = DateTime.Now;
    }   
}
