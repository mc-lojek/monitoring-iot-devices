using System.Collections.Generic;
using System.Threading.Tasks;
using dot.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace dot.Services
{

    public class IotService
    {
        private readonly IMongoCollection<Measurement> _measurementsCollection;

        public IotService(
            IOptions<IotDatabaseSettings> iotDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                iotDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                iotDatabaseSettings.Value.DatabaseName);

            _measurementsCollection = mongoDatabase.GetCollection<Measurement>(
                iotDatabaseSettings.Value.MeasurementsCollectionName);
        }

        public async Task<List<Measurement>> GetAsync() =>
            await _measurementsCollection.Find(_ => true).ToListAsync();

        public async Task<Measurement?> GetAsync(string id) =>
            await _measurementsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Measurement newMeasurement) =>
            await _measurementsCollection.InsertOneAsync(newMeasurement);

        public async Task UpdateAsync(string id, Measurement updatedMeasurement) =>
            await _measurementsCollection.ReplaceOneAsync(x => x.Id == id, updatedMeasurement);

        public async Task RemoveAsync(string id) =>
            await _measurementsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
