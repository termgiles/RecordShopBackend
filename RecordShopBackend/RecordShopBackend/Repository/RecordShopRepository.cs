using Microsoft.EntityFrameworkCore;
using RecordShopBackend;
using RecordShopBackend.Database;
using System.Linq.Expressions;

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

        public List<Album> RetrieveAlbumQuery(AlbumModification query)
        {
            try
            {
                using (_database)
                {
                    var releasedQuery = query.Released == null || query.Released == 0 ?
                        _database.Albums : _database.Albums.Where(a => a.Released == query.Released);
                    var nameQuery = query.Name == null ?
                        releasedQuery : releasedQuery.Where(a => a.Name == query.Name);
                    var artistQuery = query.Artist == null ?
                       nameQuery : nameQuery.Where(a => a.Artist == query.Artist);
                    var genreQuery = query.Genre == null ?
                        artistQuery : artistQuery.Where(a => a.Genre == query.Genre);
                    var informationQuery = query.Information == null ?
                        genreQuery : genreQuery.Where(a => a.Information == query.Information);

                    if(informationQuery.ToList<Album>().Count > 0)
                    {
                        return informationQuery.ToList<Album>();
                    }
                    else
                    {
                        throw new Exception("no albums match query");
                    }
                }
            }
            catch
            {
                return new List<Album>();
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

        public AlbumReturn CreateAlbum(Album album)
        {
            using (_database)
            {
                try
                {
                    if (_database.Albums.Any(a => a.Id == album.Id))
                    {
                        return new AlbumReturn { Found = true, ReturnedObject = album };
                    }
                    _database.Albums.Add(album);
                    _database.SaveChanges();
                    AlbumReturn newAlbum = new AlbumReturn { Found = false, ReturnedObject = album };
                    return newAlbum;
                }
                catch
                {
                    return new AlbumReturn { Found = false, ReturnedObject = null };
                }
            }
        }
    }
}
