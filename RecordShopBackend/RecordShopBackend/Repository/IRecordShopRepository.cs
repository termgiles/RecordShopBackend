
namespace RecordShopBackend.Repository
{
    public interface IRecordShopRepository
    {
        List<Album> RetrieveAllAlbums();
        public AlbumReturn RetrieveAlbumById(int id);
        public AlbumReturn UpdateAlbumById(int id, AlbumModification ammendments);
        public bool DeleteAlbumById(int id);
    }
}