namespace DataAccess.Model
{
    public class DataBaseSetting
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? CategoriesCollectionName { get; set; }
        public string? ProductsCollectionName { get; set; }
    }
}
