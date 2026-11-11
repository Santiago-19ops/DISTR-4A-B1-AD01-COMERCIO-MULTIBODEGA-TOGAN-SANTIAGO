
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Api.Models
{
    [Table("Existencia")]
    public class Existencia
    {
        [Key]
        [Column("IdExistencia")]
        public int IdExistencia { get; set; }

        [Required]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Required]
        [Column("Stock")]
        public int Stock { get; set; }
    }
}
