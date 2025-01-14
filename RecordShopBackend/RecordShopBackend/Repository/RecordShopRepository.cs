using RecordShopBackend;
using RecordShopBackend.Database;

namespace RecordShopBackend.Repository
{
    public class RecordShopRepository : IRecordShopRepository
    {
        private RecordShopDbContext _database;
        public RecordShopRepository(RecordShopDbContext db)
        {
            _database = db;
        }
        public List<Album> RetrieveAllAlbums()
        {
            using (_database)
            {
                List<Album> albums = _database.Albums.ToList();
                return albums;
            }
        }
    }
}
