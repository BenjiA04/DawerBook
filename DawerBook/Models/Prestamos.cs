using System.ComponentModel.DataAnnotations;

namespace DawerBook.Models
{
    public class Prestamos
    {
        // Propiedades
        [Key]
        public int Id { get; set; }

        // Relacion con la tabla
        public int UsuarioId { get; set; }
        public Usuarios Usuario { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_Prestamo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_Devolucion { get; set; }

        [Required]
        public string Estado { get; set; }

        public List<DetallePrestamo> Detalles { get; set; } = new();
    }
}
