using Microsoft.EntityFrameworkCore;

namespace LosDragones.DAL
{
    public class LosDragonesDBContext : DbContext
    {
        public LosDragonesDBContext() { }

        public LosDragonesDBContext(DbContextOptions<LosDragonesDBContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer();
        }

    }
}
