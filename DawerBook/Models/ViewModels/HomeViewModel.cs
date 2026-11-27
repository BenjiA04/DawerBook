namespace DawerBook.Models.ViewModels
{
    public class HomeViewModel
    {
        public int TotalLibros { get; set; }

        public int TotalUsuarios { get; set; }

        public int PrestamosActivos { get; set; }

        public int LibrosDisponibles { get; set; }

        public List<Prestamos> PrestamosRecientes { get; set; } = new();

        public List<LibrosPorGeneroViewModel> LibrosPorGenero { get; set; } = new();
    }

    public class LibrosPorGeneroViewModel
    {
        public string Genero { get; set; } = string.Empty;

        public int Cantidad { get; set; }
    }
}