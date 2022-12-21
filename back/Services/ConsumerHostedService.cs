using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace dot.Services
{
    public class ConsumerHostedService : BackgroundService
    {
        private readonly ConsumerService _consumerService;

        public ConsumerHostedService(ConsumerService consumerService)
        {
            _consumerService = consumerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumerService.ReadMessgaes();
        }
    }
}