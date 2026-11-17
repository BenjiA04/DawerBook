using System.ComponentModel.DataAnnotations;

namespace DawerBook.Models
{
    public class Usuarios
    {
        // Propiedades

        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(20)]
        public string Carnet { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

    }
}
