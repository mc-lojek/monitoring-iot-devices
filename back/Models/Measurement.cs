using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace dot.Models
{

    public class Measurement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public float Value { get; set; } = 0.0f;
        public string? SensorId { get; set; }
        public string? Type { get; set; }
        public string? Unit { get; set; }
    }
}
