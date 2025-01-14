using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RecordShopBackend;
namespace RecordShopBackend.Database
{
    public class RecordShopDbContext : DbContext
    {
        public RecordShopDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Album> Albums { get; set; }


    }
}
