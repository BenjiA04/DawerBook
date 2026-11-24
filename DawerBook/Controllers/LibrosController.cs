using DawerBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DawerBook.Controllers
{
    public class LibrosController : Controller
    {
        private readonly MyDbContext _context;

        public LibrosController(MyDbContext context)
        {
            _context = context;
        }

        // PARA BUSCAR LIBROS
        [HttpGet]
        public async Task<IActionResult> Index(string? buscar)
        {
            // Prepara la consulta de libros
            var libros = _context.Libros.AsQueryable();

            // Filtra por título si hay una búsqueda
            if (!string.IsNullOrWhiteSpace(buscar))
            {
                libros = libros.Where(l => l.Titulo.Contains(buscar.ToUpper()));
            }

            // Ejecuta la consulta y envía los resultados a la vista
            return View(await libros.ToListAsync());
        }

        // MOSTRAR FORMULARIO DE REGISTRO
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // GUARDAR LIBRO
        [HttpPost]
        public async Task<IActionResult> Crear(Libros libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            if (libro.Cantidad <= 0)
            {
                ModelState.AddModelError("Cantidad", "No puedes agregar un libro en 0.");
                return View(libro);
            }

            // Eliminar espacios innecesarios y colocar en mayusculas
            libro.Titulo = libro.Titulo.Trim().ToUpper();
            libro.Autor = libro.Autor.Trim().ToUpper();
            libro.Genero = libro.Genero.Trim().ToUpper();

            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // MOSTRAR FORMULARIO DE EDICIÓN
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            // Busca el libro por su ID
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GUARDAR CAMBIOS
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Libros libro)
        {
            if (!ModelState.IsValid)
            {
                return View(libro);
            }

            // Busca el libro original
            var libroBuscado = await _context.Libros.FindAsync(id);

            if (libroBuscado == null)
            {
                return NotFound();
            }

            // Actualiza sus propiedades
            libroBuscado.Titulo = libro.Titulo.Trim().ToUpper();
            libroBuscado.Autor = libro.Autor.Trim().ToUpper();
            libroBuscado.Genero = libro.Genero.Trim().ToUpper();
            libroBuscado.Cantidad = libro.Cantidad;
            libroBuscado.Estado = libro.Estado;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // PARA ELIMINAR LIBRO
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Buscar el libro
            var libro = await _context.Libros.FindAsync(id);

            if (libro == null)
            {
                return NotFound();
            }

            // Verificar si tiene préstamos relacionados
            bool tienePrestamos = await _context.DetallesPrestamo
                .AnyAsync(d => d.LibroId == id);

            if (tienePrestamos)
            {
                TempData["Error"] = "No puedes eliminar un libro con historial de préstamos.";

                return RedirectToAction(nameof(Index));
            }

            // Eliminar el libro
            _context.Libros.Remove(libro);

            await _context.SaveChangesAsync();

            TempData["Exito"] = "Libro eliminado correctamente.";

            return RedirectToAction(nameof(Index));
        }
    }
}
