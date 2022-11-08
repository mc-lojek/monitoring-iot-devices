using System.Collections.Generic;
using System.Threading.Tasks;
using dot.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dot.Services
{

    public class SensorService
    {
        private readonly IMongoCollection<Sensor> _sensorsCollection;

        public SensorService(
            IOptions<IotDatabaseSettings> iotDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iotDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iotDatabaseSettings.Value.DatabaseName);

            _sensorsCollection = mongoDatabase.GetCollection<Sensor>(
                iotDatabaseSettings.Value.SensorsCollectionName);
        }

        public async Task<List<Sensor>> GetAsync() =>
            await _sensorsCollection.Find(_ => true).ToListAsync();

        public async Task<Sensor?> GetAsync(string id) =>
            await _sensorsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Sensor newMeasurement) =>
            await _sensorsCollection.InsertOneAsync(newMeasurement);

        public async Task UpdateAsync(string id, Sensor updatedMeasurement) =>
            await _sensorsCollection.ReplaceOneAsync(x => x.Id == id, updatedMeasurement);

        public async Task RemoveAsync(string id) =>
            await _sensorsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
