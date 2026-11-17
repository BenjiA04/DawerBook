using System.ComponentModel.DataAnnotations;

namespace DawerBook.Models
{
    public class Libros
    {
        // Propiedades

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titulo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Genero { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        [MaxLength(50)]
        public string Autor { get; set; }
    }
}
