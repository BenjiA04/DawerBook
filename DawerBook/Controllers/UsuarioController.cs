using DawerBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DawerBook.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly MyDbContext _context;

        public UsuariosController(MyDbContext context)
        {
            _context = context;
        }

        // CONSULTAR Y BUSCAR USUARIOS
        [HttpGet]
        public async Task<IActionResult> Index(string? buscar)
        {
            // Prepara la consulta
            var usuarios = _context.Usuarios.AsQueryable();

            // Aquí agregaremos el filtro cuando confirmemos
            // las propiedades del modelo Usuarios.

            // Ejecuta la consulta y envía los resultados
            return View(await usuarios.ToListAsync());
        }

        // MOSTRAR FORMULARIO DE REGISTRO
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        // REGISTRAR USUARIO
        [HttpPost]
        public async Task<IActionResult> Crear(Usuarios usuario)
        {
            // Comprueba las validaciones del modelo
            if (!ModelState.IsValid)
            {
                return View(usuario);
            }

            // Agrega el usuario
            _context.Usuarios.Add(usuario);

            // Guarda en la base de datos
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // MOSTRAR FORMULARIO DE EDICIÓN
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            // Busca el usuario por su ID
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GUARDAR CAMBIOS DEL USUARIO
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Usuarios datos)
        {
            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            // Busca el usuario original
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Aquí actualizaremos las propiedades permitidas
            // cuando confirmemos el modelo Usuarios.

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ELIMINAR USUARIO
        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            // Busca el usuario
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            // Comprueba si tiene préstamos registrados
            bool tienePrestamos = await _context.Prestamos
                .AnyAsync(p => p.UsuarioId == id);

            if (tienePrestamos)
            {
                TempData["Error"] = "No se puede eliminar un usuario con historial de préstamos.";

                return RedirectToAction(nameof(Index));
            }

            // Elimina el usuario
            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
