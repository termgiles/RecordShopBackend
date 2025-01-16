using RecordShopBackend.Repository;

namespace RecordShopBackend.Service
{
    public class RecordShopService: IRecordShopService
    {
        private IRecordShopRepository _repository;
        public RecordShopService (IRecordShopRepository repository)
        {
            _repository = repository;
        }

        public List<Album> ReturnAllAlbums()
        {
            return _repository.RetrieveAllAlbums();
        }

        public AlbumReturn ReturnAlbumById(int id)
        {
            return _repository.RetrieveAlbumById(id);
        }

        public AlbumReturn AmmendAlbumById(int id, AlbumModification ammendments)
        {
            return _repository.UpdateAlbumById(id,ammendments);
        }

        public bool RemoveAlbumById(int id)
        {
            return _repository.DeleteAlbumById(id);
        }

        public AlbumReturn AddAlbum(Album album)
        {
            if (album.Id == 0)
            {
                album.Id = new Random().Next(100000000, 1000000000);
            }
            if (AlbumValid(album))
            {
                return _repository.CreateAlbum(album);
            }
            return new AlbumReturn { Found = true };
        }

        public List<Album> ParseQuery(string? name, string? artist, int? released, string? genre, string? information)
        {
            AlbumModification query = new AlbumModification { Artist = artist, Genre = genre, Information = information, Name = name, Released = released };

            return _repository.RetrieveAlbumQuery(query);
        }
        public string ReturnWelcomeMessage()
        {
            return "Welcome to the Record Shop Database";
        }

        public static bool AlbumValid(Album album)
        {
            if (album.Id == 0) return false;
            if (album.Artist == null || album.Artist == "") return false;
            if (album.Genre == null || album.Genre == "") return false;
            if (album.Name == null || album.Name == "") return false;
            if (album.Artist == null || album.Artist == "") return false;
            if (album.Information == null || album.Information == "") return false;
            if (album.Released == null || album.Released > (int)DateTime.UtcNow.Year) return false;

            return true;
        }

        
    }
}
