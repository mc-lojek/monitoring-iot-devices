using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dot.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
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

        public async Task<List<Measurement>> GetAsync(QueryParameters parameters)
        {
            var filter = Builders<Measurement>.Filter.Empty;

            if (!string.IsNullOrEmpty(parameters.SensorId))
            {
                filter &= Builders<Measurement>.Filter.Regex("SensorId", new BsonRegularExpression(parameters.SensorId, "i"));
            }

            if (!string.IsNullOrEmpty(parameters.Type))
            {
                filter &= Builders<Measurement>.Filter.Regex("Type", new BsonRegularExpression(parameters.Type, "i"));
            }

            if (!string.IsNullOrEmpty(parameters.Since))
            {
                filter &= Builders<Measurement>.Filter.Gte("Date", DateTime.Parse(parameters.Since));
            }
            
            if (!string.IsNullOrEmpty(parameters.Until))
            {
                filter &= Builders<Measurement>.Filter.Lte("Date", DateTime.Parse(parameters.Until));
            }
            
            var desc = 1;
            var order = parameters.OrderBy;
            if (parameters.OrderBy.StartsWith("-"))
            {
                order = order.Remove(0, 1);
                desc = -1;
            }
            
            return await _measurementsCollection.Find(filter)
                .Sort("{" + order + ": " + desc + "}")
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Limit(parameters.PageSize)
                .ToListAsync();

        }
            
        public async Task<Measurement?> GetAsync(string id) =>
            await _measurementsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Measurement newMeasurement) =>
            await _measurementsCollection.InsertOneAsync(newMeasurement);

        public async Task UpdateAsync(string id, Measurement updatedMeasurement) =>
            await _measurementsCollection.ReplaceOneAsync(x => x.Id == id, updatedMeasurement);

        public async Task RemoveAsync(string id) =>
            await _measurementsCollection.DeleteOneAsync(x => x.Id == id);

        public List<Measurement> GetSync(QueryParameters parameters)
        {
            var filter = Builders<Measurement>.Filter.Empty;

            if (!string.IsNullOrEmpty(parameters.SensorId))
            {
                filter &= Builders<Measurement>.Filter.Regex("SensorId", new BsonRegularExpression(parameters.SensorId, "i"));
            }

            if (!string.IsNullOrEmpty(parameters.Type))
            {
                filter &= Builders<Measurement>.Filter.Regex("Type", new BsonRegularExpression(parameters.Type, "i"));
            }

            if (!string.IsNullOrEmpty(parameters.Since))
            {
                filter &= Builders<Measurement>.Filter.Gte("Date", DateTime.Parse(parameters.Since));
            }
            
            if (!string.IsNullOrEmpty(parameters.Until))
            {
                filter &= Builders<Measurement>.Filter.Lte("Date", DateTime.Parse(parameters.Until));
            }
            
            var desc = 1;
            var order = parameters.OrderBy;
            if (parameters.OrderBy.StartsWith("-"))
            {
                order = order.Remove(0, 1);
                desc = -1;
            }

            
            return _measurementsCollection.Find(filter)
                .Sort("{" + order + ": " + desc + "}")
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Limit(parameters.PageSize)
                .ToList();
        }
    }
}
