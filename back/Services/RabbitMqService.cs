using System;
using dot.Config;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace dot.Services
{
    public class RabbitMqService
    {
        private readonly RabbitMqConfiguration _configuration;
        public RabbitMqService(IOptions<RabbitMqConfiguration> options)
        {
            _configuration = options.Value;
        }
        public IConnection CreateChannel()
        {
            while (true)
            {
                try
                {
                    ConnectionFactory connection = new ConnectionFactory()
                    {
                        UserName = _configuration.Username,
                        Password = _configuration.Password,
                        HostName = _configuration.HostName
                    };
                    connection.DispatchConsumersAsync = true;
                    var channel = connection.CreateConnection();
                    return channel;
                }
                catch (Exception e)
                {
                    ConnectionFactory connection = new ConnectionFactory()
                    {
                        UserName = _configuration.Username,
                        Password = _configuration.Password,
                        HostName = _configuration.HostName
                    };
                    
                    
                    Console.WriteLine("mamy klopot z rabbitem");
                    Console.WriteLine("connection: " + connection.Uri);
                }    
            }
            
        }
    }
}