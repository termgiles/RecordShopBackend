namespace RecordShopBackend
{
    public record AlbumModification 
    {
        public string? Name { get; set; } = null;
        public string? Artist { get; set; } = null;
        public int? Released { get; set; } = null;
        public string? Genre { get; set; } = null;
        public string? Information { get; set; } = null;
    }
}
