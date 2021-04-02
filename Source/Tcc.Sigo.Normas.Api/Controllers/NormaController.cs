using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        [HttpPost]
        [ProducesResponseType(typeof(NormaPost), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody][Required] NormaPost normaPost)
        {
            var normaEntity = _mapper.Map<NormaPost, NormaEntity>(normaPost);
            await _normaService.Inserir(normaEntity);

            return Created("", "");
        }
    }
}
