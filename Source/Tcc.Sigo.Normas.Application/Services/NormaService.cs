using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Entities;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Repositories;
using Tcc.Sigo.Normas.Domain.Services;
using Tcc.Sigo.Normas.Repository;

namespace Tcc.Sigo.Normas.Application.Services
{
    public class NormaService : INormaService
    {
        private readonly ILogger<NormaService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INormaReadOnlyRepository _normaReadOnlyRepository;
        private readonly INormaWriteOnlyRepository _normaWriteOnlyRepository;
        private readonly IMomAdapter _momAdapter;

        public NormaService(ILogger<NormaService> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            INormaReadOnlyRepository normaReadOnlyRepository,
            INormaWriteOnlyRepository normaWriteOnlyRepository,
            IMomAdapter momAdapter)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _normaReadOnlyRepository = normaReadOnlyRepository;
            _normaWriteOnlyRepository = normaWriteOnlyRepository;
            _momAdapter = momAdapter;
        }

        public async Task<NormaModel> ObterPor(Guid id)
        {
            return await _normaReadOnlyRepository.ObterPor(id);
        }

        public async Task Inserir(NormaEntity normaEntity) 
        {
            if (normaEntity.Valid)
            {
                var normaModel = _mapper.Map<NormaEntity, NormaModel>(normaEntity);

                _unitOfWork.BeginTransaction();

                try
                {
                    await _normaWriteOnlyRepository.Incluir(normaModel);
                    await _momAdapter.Publicar(normaModel);

                    _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    _unitOfWork.Rollback();
                }
            }
        }
    }
}
