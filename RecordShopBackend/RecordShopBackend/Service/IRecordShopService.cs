namespace RecordShopBackend.Service
{
    public interface IRecordShopService
    {
        public List<Album> ReturnAllAlbums();
        public AlbumReturn ReturnAlbumById(int id);
        public string ReturnWelcomeMessage();

    }
}
