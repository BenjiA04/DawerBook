using System.ComponentModel.DataAnnotations;

namespace DawerBook.Models
{
    public class DetallePrestamo
    {
        // Propiedades 

        [Key]
        public int Id { get; set; }

        // Relacion
        public int PrestamoId { get; set; }
        public Prestamos Prestamo { get; set; } = null!;

        public int LibroId { get; set; }
        public Libros Libro { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha_Devolucion { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Fecha_Devuelto { get; set; }
    }
}
