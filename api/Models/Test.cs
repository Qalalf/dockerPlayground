using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace api.Models
{
    public class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string item {get; set; }
        public int qty {get; set; }
        public string tags {get; set; }
        public string size {get; set; }
    }

}