using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Messages;

namespace Tcc.Sigo.Normas.MomAdapter.Acl
{
    public class AclAdapter : IAclAdapter
    {
        private readonly ILogger<AclAdapter> _logger;
        private readonly RestClient _client;

        public AclAdapter(ILogger<AclAdapter> logger)
        {
            _logger = logger;

            _client = new RestClient("http://tcc-sigo-normas-acl.azurewebsites.net");

        }

        public async Task<bool> Post(NormaMessage normaMessage)
        {
            try
            {
                var request = new RestRequest("/Tcc.Sigo.Normas.Acl/normas-acl?api_key=d02767c55a8e4ac1848cc22ea23c8811", Method.POST)
                {
                    RequestFormat = DataFormat.Json
                };

                request.AddParameter("text/json", JsonSerializer.Serialize(normaMessage), ParameterType.RequestBody);

                _ = await _client.ExecuteAsync(request);

                return true;
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Erro ao enviar as informações para a ACL {ex.Message}");

                throw;
            }
        }
    }
}
