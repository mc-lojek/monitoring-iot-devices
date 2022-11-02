using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dot.Models
{

    public class Measurement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public float value { get; set; } = 0.0f;
    }
}