using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models
{
    public class KlinikaContext : DbContext
    {
        public DbSet<Soba> Sobe { get; set; }
        public DbSet<Pacijent> Pacijenti { get; set; }
        //public DbSet<Lekar> Lekari { get; set; }
        public DbSet<Klinika> Klinike { get; internal set; }

        public KlinikaContext(DbContextOptions options) : base(options)
        {
            
        }
    } 
}