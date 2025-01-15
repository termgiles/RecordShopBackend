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

        public AlbumReturn RetrieveAlbumById(int id)
        {
            try
            {
                using (_database)
                {
                    Album album = _database.Albums.
                        Where(a => a.Id == id).First();
                    return new AlbumReturn { Found = true, ReturnedObject = album };
                }
            }
            catch
            {
                return new AlbumReturn { Found = false, ReturnedObject = null };
            }
        }

        public AlbumReturn UpdateAlbumById(int id, AlbumModification ammendments)
        {
            using (_database)
            {
                try
                {
                    if(_database.Albums.Any(a => a.Id == id))
                    {
                        Album albumToPut = _database.Albums.First(a => a.Id == id);
                        _database.Albums.Remove(albumToPut);
                        _database.SaveChanges();

                        if (ammendments.Name != null) albumToPut.Name = ammendments.Name;
                        if (ammendments.Artist != null) albumToPut.Artist = ammendments.Artist;
                        if (ammendments.Released != null) albumToPut.Released = (int)ammendments.Released;
                        if (ammendments.Genre != null) albumToPut.Genre = ammendments.Genre;
                        if (ammendments.Information != null) albumToPut.Information = ammendments.Information;

                        _database.Albums.Add(albumToPut);
                        _database.SaveChanges();
                        return new AlbumReturn { Found = true, ReturnedObject = albumToPut };
                    }
                    else
                    {
                        throw new Exception("album not found");
                    }
                }
                catch
                {
                    return new AlbumReturn { Found = false, ReturnedObject = null };
                }
            }
        }

        public bool DeleteAlbumById(int id)
        {
            using (_database)
            {
                try
                {
                    if (_database.Albums.Any(a => a.Id == id))
                    {
                        Album albumToDelete = _database.Albums.First(a => a.Id == id);
                        _database.Albums.Remove(albumToDelete);
                        _database.SaveChanges();

                        return true;
                    }
                    else
                    {
                        throw new Exception("album not found");
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
