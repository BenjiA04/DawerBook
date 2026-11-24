using DawerBook.Models;
using DawerBook.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DawerBook.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly MyDbContext _context;

        public PrestamosController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Consulta los préstamos con sus usuarios y libros
            var prestamos = await _context.Prestamos
                .Include(p => p.Usuario) // datos del usuario que realizó el préstamo
                .Include(p => p.Detalles) // detalles de cada préstamo
                    .ThenInclude(d => d.Libro) // información de los libros de cada detalle
                .ToListAsync();

            return View(prestamos);
        }

        // MOSTRAR FORMULARIO DE REGISTRO
        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            // Obtiene usuarios y libros para el formulario
            ViewBag.Usuarios = await _context.Usuarios.ToListAsync();

            ViewBag.Libros = await _context.Libros
                .Where(l => l.Cantidad > 0)
                .ToListAsync();

            return View(new CrearPrestamoViewModel());
        }

        // REGISTRAR PRÉSTAMO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearPrestamoViewModel datos)
        {
            // Comprueba las fechas
            if (datos.FechaDevolucion < datos.FechaPrestamo)
            {
                ModelState.AddModelError(
                    "FechaDevolucion",
                    "La devolución no puede ser anterior al préstamo.");
            }

            // Comprueba que se seleccionó algún libro
            if (datos.LibrosIds == null || datos.LibrosIds.Count == 0)
            {
                ModelState.AddModelError(
                    "LibrosIds",
                    "Debes seleccionar al menos un libro.");
            }

            // Comprueba que el usuario existe
            bool usuarioExiste = await _context.Usuarios
                .AnyAsync(u => u.Id == datos.UsuarioId);

            if (!usuarioExiste)
            {
                ModelState.AddModelError(
                    "UsuarioId",
                    "El usuario seleccionado no existe.");
            }

            // Evita seleccionar el mismo libro varias veces
            var idsLibros = datos.LibrosIds?.Distinct().ToList()
                            ?? new List<int>();

            // Busca los libros seleccionados
            var libros = await _context.Libros
                .Where(l => idsLibros.Contains(l.Id))
                .ToListAsync();

            // Comprueba que todos existen y tienen disponibilidad
            if (libros.Count != idsLibros.Count ||
                libros.Any(l => l.Cantidad <= 0))
            {
                ModelState.AddModelError(
                    "LibrosIds",
                    "Uno o varios libros no están disponibles.");
            }

            // Si hay errores, vuelve al formulario
            if (!ModelState.IsValid)
            {
                ViewBag.Usuarios = await _context.Usuarios.ToListAsync();

                ViewBag.Libros = await _context.Libros
                    .Where(l => l.Cantidad > 0)
                    .ToListAsync();

                return View(datos);
            }

            // Crea el préstamo principal
            var prestamo = new Prestamos
            {
                UsuarioId = datos.UsuarioId,
                Fecha_Prestamo = datos.FechaPrestamo,
                Fecha_Devolucion = datos.FechaDevolucion,
                Estado = "ACTIVO"
            };

            // Crea un detalle por cada libro seleccionado
            foreach (var libro in libros)
            {
                prestamo.Detalles.Add(new DetallePrestamo
                {
                    LibroId = libro.Id,
                    Fecha_Devolucion = datos.FechaDevolucion
                });

                // Descuenta una unidad del inventario
                libro.Cantidad--;

                // Actualiza el estado según la disponibilidad
                libro.Estado = libro.Cantidad > 0
                    ? "DISPONIBLE"
                    : "NO DISPONIBLE";
            }

            // Guarda el préstamo y sus detalles
            _context.Prestamos.Add(prestamo);

            await _context.SaveChangesAsync();

            TempData["Exito"] = "Préstamo registrado correctamente.";

            return RedirectToAction(nameof(Index));
        }

        // REGISTRAR DEVOLUCIÓN
        [HttpPost]
        public async Task<IActionResult> Devolver(int id)
        {
            // Busca el préstamo con sus detalles
            var prestamo = await _context.Prestamos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (prestamo == null)
            {
                return NotFound();
            }

            // Busca los libros que todavía no se han devuelto
            var pendientes = prestamo.Detalles
                .Where(d => d.Fecha_Devuelto == null)
                .ToList();

            if (pendientes.Count == 0)
            {
                TempData["Error"] = "Este préstamo ya fue devuelto.";

                return RedirectToAction(nameof(Index));
            }

            foreach (var detalle in pendientes)
            {
                // Registra la fecha real de devolución
                detalle.Fecha_Devuelto = DateTime.Today;

                // Recupera una unidad en el inventario
                var libro = await _context.Libros.FindAsync(detalle.LibroId);

                if (libro != null)
                {
                    libro.Cantidad++;

                    // Actualiza su estado
                    libro.Estado = "DISPONIBLE";
                }
            }

            // Actualiza el estado del préstamo
            prestamo.Estado = "DEVUELTO";

            await _context.SaveChangesAsync();

            TempData["Exito"] = "Préstamo devuelto correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}