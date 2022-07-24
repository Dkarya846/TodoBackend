using MongoDB.Bson.Serialization.Attributes;

namespace TodoBackend.Models
{
    public class Todo
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Desc { get; set; } = string.Empty;
        public int Status { get; set; } = 0;
    }
}
