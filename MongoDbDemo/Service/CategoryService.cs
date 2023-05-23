using DataAccess.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace MongoDbDemo.Service
{
    public class CategoryService: ICategoryService
    {
        private readonly IMongoCollection<Category> _catogoryCollection;
        private readonly IOptions<DataBaseSetting> _dbSetting;
        public CategoryService(IOptions<DataBaseSetting> dbSetting)
        {
            _dbSetting = dbSetting;
            var mongoClient = new MongoClient(dbSetting.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSetting.Value.DatabaseName);
            _catogoryCollection = mongoDatabase.GetCollection<Category>
                   (dbSetting.Value.CategoriesCollectionName);
        }

        public async Task<IEnumerable<Category>> GetAllAsync() =>
            await _catogoryCollection.Find(_ => true).ToListAsync();

        public async Task<Category> GetByIdAsync(string id) =>
            await _catogoryCollection.Find(a=> a.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync (Category category)=>
            await _catogoryCollection.InsertOneAsync(category);

        public async Task UpdateAsync( string id ,Category category) =>
            await _catogoryCollection.ReplaceOneAsync(a => a.Id == id, category);

        public async Task DeleteAsync (string id)=>
            await _catogoryCollection.DeleteOneAsync(a=> a.Id == id);
    }
}
