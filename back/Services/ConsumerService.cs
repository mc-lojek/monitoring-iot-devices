using System;
using System.Threading.Tasks;
using dot.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace dot.Services
{
    public class ConsumerService : IDisposable
    {
        private readonly IModel _model;
        private readonly IConnection _connection;

        public ConsumerService(RabbitMqService rabbitMqService, IOptions<IotDatabaseSettings> iotDatabaseSettings)
        {
            _connection = rabbitMqService.CreateChannel();
            _model = _connection.CreateModel();
            _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
            _model.ExchangeDeclare("SensorsExchange", ExchangeType.Direct, durable: true, autoDelete: false);
            _model.QueueBind(_queueName, "SensorsExchange", string.Empty);
            
            var mongoClient = new MongoClient(
                iotDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                iotDatabaseSettings.Value.DatabaseName);
            _measurementsCollection = mongoDatabase.GetCollection<Measurement>(
                iotDatabaseSettings.Value.MeasurementsCollectionName);
        }

        const string _queueName = "Sensors";

        private readonly IMongoCollection<Measurement> _measurementsCollection;
        
        public async Task ReadMessgaes()
        {
            var consumer = new AsyncEventingBasicConsumer(_model);
            consumer.Received += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var text = System.Text.Encoding.UTF8.GetString(body);
                    var elements = text.Split(';');
                    var measurement = new Measurement
                    {
                        Date = DateTime.Parse(elements[0]),
                        Value = float.Parse(elements[1]),
                        SensorId = elements[2],
                        Type = elements[3],
                        Unit = elements[4]
                    };

                    _measurementsCollection.InsertOne(measurement);
                    
                    Console.WriteLine(text);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
                await Task.CompletedTask;
                _model.BasicAck(ea.DeliveryTag, false);
            };
            _model.BasicConsume(_queueName, false, consumer);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_model.IsOpen)
                _model.Close();
            if (_connection.IsOpen)
                _connection.Close();
        }
    }
}