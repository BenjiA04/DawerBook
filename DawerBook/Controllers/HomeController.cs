using System.Diagnostics;
using DawerBook.Models;
using DawerBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DawerBook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dashboard = new HomeViewModel
            {
                // Total de ejemplares registrados
                TotalLibros = await _context.Libros
                    .SumAsync(l => l.Cantidad),

                // Total de usuarios registrados
                TotalUsuarios = await _context.Usuarios
                    .CountAsync(),

                // Total de préstamos activos
                PrestamosActivos = await _context.Prestamos
                    .CountAsync(p => p.Estado == "ACTIVO"),

                // Total de ejemplares disponibles
                LibrosDisponibles = await _context.Libros
                    .Where(l => l.Estado == "DISPONIBLE")
                    .SumAsync(l => l.Cantidad),

                // Últimos cinco préstamos registrados
                PrestamosRecientes = await _context.Prestamos
                    .Include(p => p.Usuario)
                    .Include(p => p.Detalles)
                    .ThenInclude(d => d.Libro)
                    .OrderByDescending(p => p.Fecha_Prestamo)
                    .ThenByDescending(p => p.Id)
                    .Take(5)
                    .ToListAsync(),

                // Agrupación de libros por género
                LibrosPorGenero = await _context.Libros
                    .GroupBy(l => l.Genero)
                    .Select(g => new LibrosPorGeneroViewModel
                    {
                        Genero = g.Key,
                        Cantidad = g.Sum(l => l.Cantidad)
                    })
                    .OrderByDescending(g => g.Cantidad)
                    .ToListAsync()
            };

            return View(dashboard);
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult AccesoDenegado()
        {
            return StatusCode(403, "No tienes permiso para acceder a esta opción.");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}