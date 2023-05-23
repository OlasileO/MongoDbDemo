using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DataAccess.Model
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
    }
}
