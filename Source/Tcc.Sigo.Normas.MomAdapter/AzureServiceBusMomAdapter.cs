using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Messages;
using Tcc.Sigo.Normas.Domain.Models;


namespace Tcc.Sigo.Normas.MomAdapter
{
    public class AzureServiceBusMomAdapter : IMomAdapter
    {
        private readonly IConfiguration _configuration;
        private readonly IAclAdapter _aclAdapter;

        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "tccsigonormas";

        public AzureServiceBusMomAdapter(IConfiguration configuration, IAclAdapter aclAdapter) 
        {
            _configuration = configuration;
            _aclAdapter = aclAdapter;

            _queueClient = new QueueClient(
            _configuration.GetConnectionString("AzureServiceBusMomConnectionString"),
              QUEUE_NAME);
        }

        public async Task Publicar(NormaMessage normaMessage)
        {
            string data = JsonSerializer.Serialize(normaMessage);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);

            RegistrarConsumo();
        }

        private void RegistrarConsumo()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionHandler) { AutoComplete = false };

            _queueClient.RegisterMessageHandler(ProcessMessageHandler, messageHandlerOptions);
        }

        private async Task ProcessMessageHandler(Message message, CancellationToken cancellationToken)
        {
            var messageString = Encoding.UTF8.GetString(message.Body);
            var normaMessage = JsonSerializer.Deserialize<NormaMessage>(messageString);

            var entregouMensagem = await _aclAdapter.Post(normaMessage);
            if (entregouMensagem)
                await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionHandler(ExceptionReceivedEventArgs arg)
        {
            return Task.CompletedTask;
        }
    }
}
