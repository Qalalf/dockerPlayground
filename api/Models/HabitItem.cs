using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api.Models
{
    public class HabitItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string Id { get; set; }

        public string Anchor { get; set; }
        public string Microhabit { get; set; }
        public string Celebration { get; set; }
    }
}