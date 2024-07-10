using Dal.Dragones;
using Microsoft.EntityFrameworkCore;

namespace Bar_Restaurante_Los_Dragones.Models
{
    public class ProyectoContext : DbContext
    {

        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }
        public DbSet<Plato> Platos { get; set; }
        public DbSet<Factura> Facturas { get; set; }


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


            modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Mesa)
            .WithMany(m => m.Pedidos)
            .HasForeignKey(p => p.IdMesa);

            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.Detalles)
                .HasForeignKey(dp => dp.IdPedido);


            modelBuilder.Entity<DetallePedido>()
            .Property(d => d.Precio)
            .HasColumnType("decimal(18,2)");

            // Configuración de Pedido
            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasColumnType("decimal(18,2)");

            // Configuración de Plato
            modelBuilder.Entity<Plato>()
                .Property(p => p.Precio)
                .HasColumnType("decimal(18,2)");

            // Seed data (opcional)
            modelBuilder.Entity<Mesa>().HasData(
                new Mesa { Id = 1, NumMesa = 1, Estado = "Disponible" },
                new Mesa { Id = 2, NumMesa = 2, Estado = "Disponible" },
                new Mesa { Id = 3, NumMesa = 3, Estado = "Disponible" }
            );

            modelBuilder.Entity<Plato>().HasData(
                new Plato { Id = 1, Nombre = "AJI DE GALLINA", Precio = 10.00m, Disponible = true },
                new Plato { Id = 2, Nombre = "CEBICHE", Precio = 25.00m, Disponible = true },
                new Plato { Id = 3, Nombre = "ARROZ CON POLLO", Precio = 8.00m, Disponible = true }
            );

            modelBuilder.Entity<Factura>()
            .HasOne(p => p.Pedidos)
            .WithMany(m => m.Facturas)
            .HasForeignKey(p => p.PedidoId);


        }
    }
}
