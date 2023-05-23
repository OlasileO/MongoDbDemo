using DataAccess.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDbDemo.Service
{
    public class ProductService:IProductService
    {

        private readonly IMongoCollection<Product> _productCollection;
        private readonly IOptions<DataBaseSetting> _dbSetting;
        public ProductService(IOptions<DataBaseSetting> dbSetting)
        {
            _dbSetting = dbSetting;
            var mongoClient = new MongoClient(dbSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSetting.Value.DatabaseName);
            _productCollection = mongoDatabase.GetCollection<Product>
                   (dbSetting.Value.ProductsCollectionName);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var pipeline = new BsonDocument[]
            {
                new BsonDocument("$lookup", new BsonDocument
              {
                { "from", "CategoryName" },
                { "localField", "CategoryId" },
                { "foreignField", "_id" },
                { "as", "product_category" }
              }),
              new BsonDocument("$unwind", "$product_category"),
              new BsonDocument("$project", new BsonDocument
              {
                { "_id", 1 },
                { "CategoryId", 1},
                { "ProductName",1 },
                { "CategoryName", "$product_category.CategoryName" }
              })
            };
            var result = await _productCollection.Aggregate<Product>(pipeline).ToListAsync();
            return result;
        }
           

        public async Task<Product> GetByIdAsync(string id) =>
            await _productCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _productCollection.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) =>
            await _productCollection.ReplaceOneAsync(a => a.Id == id, product);

        public async Task DeleteAsync(string id) =>
            await _productCollection.DeleteOneAsync(a => a.Id == id);
    }
}
