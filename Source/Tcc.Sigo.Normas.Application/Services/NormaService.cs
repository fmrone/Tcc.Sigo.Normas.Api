using AutoMapper;
using Flunt.Notifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Domain.Adapters;
using Tcc.Sigo.Normas.Domain.Entities;
using Tcc.Sigo.Normas.Domain.Enumerators;
using Tcc.Sigo.Normas.Domain.Messages;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Repositories;
using Tcc.Sigo.Normas.Domain.Results;
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

        public async Task<Result<NormaEntity>> Alterar(NormaEntity normaEntity)
        {
            if (normaEntity.Invalid)
                return Result<NormaEntity>.Error(normaEntity.Notifications);

            var normaModel = _mapper.Map<NormaEntity, NormaModel>(normaEntity);
            var normaMessage = _mapper.Map<NormaModel, NormaMessage>(normaModel);

            _unitOfWork.BeginTransaction();

            try
            {
                await _normaWriteOnlyRepository.Alterar(normaModel);
                    
                normaMessage.Operacao = (byte)EOperacao.Alterar;

                await _momAdapter.Publicar(normaMessage);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();

                _logger.LogError(ex.Message);

                var notifications = new List<Notification>
                {
                    new Notification("Exception", ex.Message)
                };

                return Result<NormaEntity>.Error(notifications);
            }

            return Result<NormaEntity>.Ok(normaEntity);
        }

        public async Task<Result> AtivarInativar(Guid id, bool status)
        {
            var normaExistente = await _normaReadOnlyRepository.ObterPor(id);
            if (normaExistente == null)
                return Result.Ok();

            _unitOfWork.BeginTransaction();

            try
            {
                await _normaWriteOnlyRepository.AtivarInativar(id, status);

                await _momAdapter.Publicar(
                    new NormaMessage
                    {
                        Id = id,
                        Codigo = normaExistente.Codigo,
                        Status = status,
                        Operacao = (byte)EOperacao.AtivarInativar
                    });

                _unitOfWork.Commit();


                return Result.Ok();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();

                _logger.LogError(ex.Message);

                var notifications = new List<Notification>
                {
                    new Notification("Exception", ex.Message)
                };

                return Result.Error(notifications);
            }
        }

        public async Task<Result<NormaEntity>> Inserir(NormaEntity normaEntity) 
        {
            if (normaEntity.Invalid)
                return Result<NormaEntity>.Error(normaEntity.Notifications);

            var normaModel = _mapper.Map<NormaEntity, NormaModel>(normaEntity);
            var normaMessage = _mapper.Map<NormaModel, NormaMessage>(normaModel);
            _unitOfWork.BeginTransaction();

            try
            {
                await _normaWriteOnlyRepository.Incluir(normaModel);

                normaMessage.Operacao = (byte)EOperacao.Inserir;

                await _momAdapter.Publicar(normaMessage);

                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();

                _logger.LogError(ex.Message);

                var notifications = new List<Notification>
                {
                    new Notification("Exception", ex.Message)
                };

                return Result<NormaEntity>.Error(notifications);
            }

            return Result<NormaEntity>.Ok(normaEntity);
        }

        public async Task<IEnumerable<NormaModel>> ObterPor(byte area)
        {
            return await _normaReadOnlyRepository.ObterPor(area);
        }

        public async Task<NormaModel> ObterPor(Guid id)
        {
            return await _normaReadOnlyRepository.ObterPor(id);
        }

        public async Task<IEnumerable<NormaModel>> Obter()
        {
            return await _normaReadOnlyRepository.Obter();
        }
    }
}
