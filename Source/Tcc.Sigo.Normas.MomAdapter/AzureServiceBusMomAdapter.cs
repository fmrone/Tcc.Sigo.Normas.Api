﻿using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.MomAdapter
{
    public class AzureServiceBusMomAdapter : IMomAdapter
    {
        private readonly IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "tccsigonormas";

        public AzureServiceBusMomAdapter(IConfiguration configuration) 
        {
            _configuration = configuration;

            _queueClient = new QueueClient(
              _configuration.GetConnectionString("AzureServiceBusMomConnectionString"),
              QUEUE_NAME);
        }

        public async Task Publicar(NormaModel normaModel)
        {
            string data = JsonConvert.SerializeObject(normaModel);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}