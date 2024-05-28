using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }

        [BsonElement("author")]
        public string? Author { get; set; }

        [BsonElement("genre")]
        public string? Genre { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }
    }
}
