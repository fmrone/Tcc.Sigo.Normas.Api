using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;

namespace Tcc.Sigo.Normas.Application.BackgroudServices
{
    public class AzureServiceBusBackgroundService : BackgroundService
    {
        private readonly ILogger<AzureServiceBusBackgroundService> _logger;
        private readonly IMomAdapter _momAdapter;


        public AzureServiceBusBackgroundService(IServiceScopeFactory serviceScopeFactory,
            ILogger<AzureServiceBusBackgroundService> logger) 
        {
            _logger = logger;

            using (var scope = serviceScopeFactory.CreateScope()) 
            {
                _logger = scope.ServiceProvider.GetRequiredService<ILogger<AzureServiceBusBackgroundService>>();
               _momAdapter = scope.ServiceProvider.GetRequiredService<IMomAdapter>();
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.ThrowIfCancellationRequested();
            await EscutarQueue();
        }

        private async Task EscutarQueue() 
        {
            _logger.LogInformation("Escutando fila...");

            await _momAdapter.Ler();
        }

    }
}
