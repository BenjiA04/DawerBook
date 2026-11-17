using DawerBook.Models;
using Microsoft.EntityFrameworkCore;

namespace DawerBook
{
    public class MyDbContext: DbContext
    {
        public MyDbContext( DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Libros> Libros { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Prestamos> Prestamos { get; set; }
        public DbSet<DetallePrestamo> DetallesPrestamo { get; set; }

        // Este método permite configurar cómo Entity Framework
        // manejará las relaciones entre las tablas.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // ===== RELACIÓN ENTRE PRÉSTAMOS Y USUARIOS =====

            modelBuilder.Entity<Prestamos>()       
                .HasOne(p => p.Usuario)            // Cada préstamo pertenece a un solo usuario.
                .WithMany()                        // Un usuario puede tener muchos préstamos.
                .HasForeignKey(p => p.UsuarioId)   // UsuarioId es la clave foránea (FK).
                .OnDelete(DeleteBehavior.Restrict); // Impide eliminar usuarios que tengan préstamos.


            // ===== RELACIÓN ENTRE DETALLES Y PRÉSTAMOS =====

            modelBuilder.Entity<DetallePrestamo>()
                .HasOne(d => d.Prestamo)              
                .WithMany(p => p.Detalles)            // Un préstamo puede tener varios detalles.
                .HasForeignKey(d => d.PrestamoId)     // PrestamoId será la clave foránea.
                .OnDelete(DeleteBehavior.Restrict);   // Impide eliminar préstamos con detalles.


            // ===== RELACIÓN ENTRE DETALLES Y LIBROS =====

            modelBuilder.Entity<DetallePrestamo>() 
                .HasOne(d => d.Libro)              
                .WithMany()                        // Un libro puede aparecer en varios detalles de préstamos.
                .HasForeignKey(d => d.LibroId)     // LibroId es la clave foránea (FK).
                .OnDelete(DeleteBehavior.Restrict); // Impide eliminar libros que tengan registros de préstamos.
        }
    }
}
