using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Api.Dtos;
using Tcc.Sigo.Normas.Api.Models;
using Tcc.Sigo.Normas.Domain.Entities;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Services;

namespace Tcc.Sigo.Normas.Api.Controllers
{
    [Route("normas")]
    [ApiController]
    public class NormaController : ApiControllerBase
    {
        private readonly ILogger<NormaController> _logger;
        private readonly IMapper _mapper;
        private readonly INormaService _normaService;

        public NormaController(ILogger<NormaController> logger, 
            IMapper mapper, 
            INormaService normaService)
        {
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            
            _normaService = normaService ??
                throw new ArgumentNullException(nameof(normaService));
        }

        /// <summary>
        /// Obter normas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(NormaGet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var normasModel = await _normaService.Obter();
            if (normasModel == null)
                return NotFound("Não existem normas cadastradas");

            return Ok(_mapper.Map<IEnumerable<NormaModel>, IEnumerable<NormaGet>>(normasModel));
        }

        /// <summary>
        /// Obter norma pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(NormaGet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            var normaModel = await _normaService.ObterPor(id);
            if (normaModel == null)
                return NotFound("Norma não encontrada");

            return Ok(_mapper.Map<NormaModel, NormaGet>(normaModel));
        }

        /// <summary>
        /// Obter normas pelas área
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("area/{area}")]
        [ProducesResponseType(typeof(NormaGet), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(byte area)
        {
            var normasModel = await _normaService.ObterPor(area);
            if (normasModel == null)
                return NotFound("Normas não encontradas");

            return Ok(_mapper.Map<IEnumerable<NormaModel>, IEnumerable<NormaGet>>(normasModel));
        }

        /// <summary>
        /// Inserir uma norma
        /// </summary>
        /// <param name="normaPost"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(NormaPost), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody][Required] NormaPost normaPost)
        {
            var normaEntity = _mapper.Map<NormaPost, NormaEntity>(normaPost);
            var result = await _normaService.Inserir(normaEntity);
            if (result.Success)
                return Created($"{result.Object.Id}", result.Object.Id);

            return BadRequest(result.Notifications);
        }

        /// <summary>
        /// Alterar uma norma
        /// </summary>
        /// <param name="normaPut"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(NormaPut), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody][Required] NormaPut normaPut)
        {
            var normaEntity = _mapper.Map<NormaPut, NormaEntity>(normaPut);
            var result = await _normaService.Alterar(normaEntity);
            if (result.Success)
                return Ok(result);

            return BadRequest(result.Notifications);
        }

        /// <summary>
        /// Ativar ou inativar uma norma
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>

        [HttpPatch]
        [Route("ativar-inativar/{id}/{status}")]
        [ProducesResponseType(typeof(NormaPut), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(Guid id, bool status)
        {
            var result = await _normaService.AtivarInativar(id, status);
            if (result.Success)
                return Ok(result);

            return BadRequest(result.Notifications);
        }
    }
}
