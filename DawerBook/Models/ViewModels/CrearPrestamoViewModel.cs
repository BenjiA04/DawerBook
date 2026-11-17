using System.ComponentModel.DataAnnotations;

namespace DawerBook.Models.ViewModels
{
    public class CrearPrestamoViewModel
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime FechaPrestamo { get; set; } = DateTime.Today;

        [Required]
        public DateTime FechaDevolucion { get; set; } = DateTime.Today.AddDays(7);

        // ID de los libros seleccionados
        public List<int> LibrosIds { get; set; } = new();
    }
}
