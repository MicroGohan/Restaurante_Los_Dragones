using Dal.Dragones;
using Microsoft.EntityFrameworkCore;

namespace Bar_Restaurante_Los_Dragones.Models
{
    public class ProyectoContext : DbContext
    {

        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.ID); // Asumiendo que hay una propiedad ID en tu clase Usuario
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Correo).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Clave).IsRequired().HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Rol>()
                .HasMany(r => r.Usuarios)
                .WithOne(u => u.Rol)
                .HasForeignKey(u => u.RolID);
        }
    }
}
