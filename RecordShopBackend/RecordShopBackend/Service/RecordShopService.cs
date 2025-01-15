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

        public string ReturnWelcomeMessage()
        {
            return "Welcome to the Record Shop Database";
        }
    }
}
