using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dot.Models
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Type { get; set; }
    }
}