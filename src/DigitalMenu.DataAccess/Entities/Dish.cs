using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DigitalMenu.DataAccess.Entities
{
    public class Dish
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId InternalId { get; set; }

        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public DishCategory Category { get; set; }
        public Availibility Availibility { get; set; }
        public bool IsDeactivated { get; set; }
        public TimeSpan? ApproxWaitTime { get; set; }
    }
}
