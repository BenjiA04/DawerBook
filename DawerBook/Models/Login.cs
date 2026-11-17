using System.ComponentModel.DataAnnotations;

namespace DawerBook.Models
{
    public class Login
    {
        // Propiedades
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
