using System.Security.Claims;
using DawerBook.Migrations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DawerBook.Controllers
{
    public class LoginController : Controller
    {
        private readonly MyDbContext _context;

        public LoginController(MyDbContext context)
        {
            _context = context;
        }

        // MOSTRAR FORMULARIO DE LOGIN
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        // INICIAR SESIÓN
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string usuario, string contraseña)
        {
            // Comprueba que se ingresaron los datos
            if (string.IsNullOrWhiteSpace(usuario) ||
                string.IsNullOrWhiteSpace(contraseña))
            {
                ViewBag.Error = "Completa todos los campos.";
                return View();
            }

            // Busca la cuenta y verifica las credenciales
            var cuenta = await _context.Login
                .FirstOrDefaultAsync(l => l.Username == usuario
                    && l.Password == contraseña);

            // Comprueba si las credenciales son correctas
            if (cuenta == null)
            {
                ViewBag.Error = "Usuario o contraseña incorrectos.";
                return View();
            }

            // La cuenta antigua de Bibliotecario corresponde al rol Vendedor.
            var rol = cuenta.Rol == "Bibliotecario" ? "Vendedor" : cuenta.Rol;

            // Almacena los datos de identidad del usuario
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, cuenta.Id.ToString()),
                new Claim(ClaimTypes.Name, cuenta.Username),
                new Claim(ClaimTypes.Role, rol)
            };

            // Crea la identidad del usuario
            var identidad = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            // Inicia sesión mediante una cookie
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identidad));

            // Redirige a la pantalla principal
            return RedirectToAction("Index", "Home");
        }

        // CERRAR SESIÓN
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarSesion()
        {
            // Elimina la cookie de autenticación
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }
    }
}
