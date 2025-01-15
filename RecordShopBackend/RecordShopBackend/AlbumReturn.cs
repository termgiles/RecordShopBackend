namespace RecordShopBackend
{
    public record AlbumReturn 
    {
        public required bool Found { get; set; }
        public Album? ReturnedObject {  get; set; }    
    }
}
