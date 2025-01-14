namespace RecordShopBackend.Service
{
    public interface IRecordShopService
    {
        public List<Album> ReturnAllAlbums();

        public string ReturnWelcomeMessage();
    }
}
