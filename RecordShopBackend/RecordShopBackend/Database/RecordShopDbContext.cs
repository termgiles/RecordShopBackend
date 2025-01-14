using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecordShopBackend;
namespace RecordShopBackend.Database
{
    public class RecordShopDbContext : DbContext
    {
        public RecordShopDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasKey(a => a.Id);
            modelBuilder.Entity<Album>().HasData(
                new Album { Id = 1, Artist = "L'Ed Sheeran", Genre = "La Pop", Information = "un album du chanteur Ed Sheeran nommé d'après un symbiole mathématique", Name = "L'addition", Released = 2011 },
                new Album { Id = 2, Artist = "L'Ed Sheeran", Genre = "La Pop", Information = "un album du chanteur Ed Sheeran nommé d'après un symbiole mathématique", Name = "La multiplication", Released = 2014 }
            );
        }


    }
}
