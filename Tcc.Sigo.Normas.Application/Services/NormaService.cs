using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Repositories;
using Tcc.Sigo.Normas.Domain.Services;

namespace Tcc.Sigo.Normas.Application.Services
{
    public class NormaService : INormaService
    {
        private readonly ILogger<NormaService> _logger;
        private readonly INormaReadOnlyRepository _normaReadOnlyRepository;
        private readonly INormaWriteOnlyRepository _normaWriteOnlyRepository;

        public NormaService(ILogger<NormaService> logger,
            INormaReadOnlyRepository normaReadOnlyRepository,
            INormaWriteOnlyRepository normaWriteOnlyRepository)
        {
            _logger = logger;
            _normaReadOnlyRepository = normaReadOnlyRepository;
            _normaWriteOnlyRepository = normaWriteOnlyRepository;
        }

        public async Task<NormaModel> ObterPor(Guid id)
        {
            return await _normaReadOnlyRepository.ObterPor(id);
        }
    }
}
