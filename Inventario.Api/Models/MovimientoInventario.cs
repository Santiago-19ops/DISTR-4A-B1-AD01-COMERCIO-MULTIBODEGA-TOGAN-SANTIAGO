
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Api.Models
{
    [Table("MovimientoInventario")]
    public class MovimientoInventario
    {
        [Key]
        [Column("IdMovimiento")]
        public int IdMovimiento { get; set; }

        [Required]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Tipo")]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; }
    }
}
