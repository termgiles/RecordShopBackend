using RecordShopBackend.Repository;

namespace RecordShopBackend.Service
{
    public class RecordShopService: IRecordShopService
    {
        private RecordShopRepository _repository;
        public RecordShopService (RecordShopRepository repository)
        {
            _repository = repository;
        }

        public List<Album> ReturnAllAlbums()
        {
            return _repository.RetrieveAllAlbums();
        }

        public string ReturnWelcomeMessage()
        {
            return "Welcome to the Record Shop Database";
        }
    }
}
