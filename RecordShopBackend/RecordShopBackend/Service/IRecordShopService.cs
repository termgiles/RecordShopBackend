namespace RecordShopBackend.Service
{
    public interface IRecordShopService
    {
        public List<Album> ReturnAllAlbums();
        public AlbumReturn ReturnAlbumById(int id);
        public AlbumReturn AmmendAlbumById(int id, AlbumModification ammendments);
        public bool RemoveAlbumById(int id);
        public string ReturnWelcomeMessage();

    }
}
