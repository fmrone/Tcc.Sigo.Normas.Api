
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Application.BackgroudServices
{
    public class AzureServiceBusConsumer
    {
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "tccsigonormas";

        public AzureServiceBusConsumer(IConfiguration configuration) 
        {
            _configuration = configuration;

            _queueClient = new QueueClient(
            _configuration.GetConnectionString("AzureServiceBusMomConnectionString"),
              QUEUE_NAME);
        }

        public void RegisterHandler()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler) { AutoComplete = false };

            _queueClient.RegisterMessageHandler(ProcessMessageHandler, messageHandlerOptions);
        }

        private async Task ProcessMessageHandler(Message message, CancellationToken cancellationToken)
        {
            var messageString = Encoding.UTF8.GetString(message.Body);
            var normaModel = JsonConvert.DeserializeObject<NormaModel>(messageString);
            

            // chama o legado

            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionHandler(ExceptionReceivedEventArgs arg)
        {
            return Task.CompletedTask;
        }
    }
}
