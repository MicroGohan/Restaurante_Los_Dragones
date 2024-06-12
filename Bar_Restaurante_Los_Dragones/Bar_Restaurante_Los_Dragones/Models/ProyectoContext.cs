using Dal.Dragones;
using Microsoft.EntityFrameworkCore;

namespace Bar_Restaurante_Los_Dragones.Models
{
    public class ProyectoContext : DbContext
    {

        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID); // Asumiendo que hay una propiedad ID en tu clase Usuario
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(100);

                // Mapear el array de roles a una cadena separada por comas en la base de datos
                entity.Property(e => e.Roles).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
