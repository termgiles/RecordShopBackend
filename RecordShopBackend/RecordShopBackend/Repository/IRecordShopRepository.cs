
namespace RecordShopBackend.Repository
{
    public interface IRecordShopRepository
    {
        List<Album> RetrieveAllAlbums();
        public AlbumReturn RetrieveAlbumById(int id);
    }
}