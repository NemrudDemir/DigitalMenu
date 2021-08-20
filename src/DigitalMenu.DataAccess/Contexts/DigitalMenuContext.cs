using DigitalMenu.DataAccess.Abstractions;
using DigitalMenu.DataAccess.Entities;
using DigitalMenu.DataAccess.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;

namespace DigitalMenu.DataAccess.Contexts
{
    public class DigitalMenuContext : IDigitalMenuContext
    {
        private readonly IMongoDatabase _database;
        public IMongoCollection<Dish> Dishes => _database.GetCollection<Dish>("Dish");

        public DigitalMenuContext(IOptions<MongoDbSettings> mongoDbSettings)
        {
            if (mongoDbSettings?.Value == null) throw new ArgumentNullException(nameof(mongoDbSettings));

            var settings = MongoClientSettings.FromConnectionString(mongoDbSettings.Value.ConnectionString);
            var client = new MongoClient(settings);
            _database = client.GetDatabase(mongoDbSettings.Value.DatabaseName);
        }
    }
}