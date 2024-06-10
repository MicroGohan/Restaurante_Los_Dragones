using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bar_Restaurante_Los_Dragones.Models
{
    public class AuthContext : IdentityDbContext
    {
        public AuthContext() { }

        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }
    }
}

