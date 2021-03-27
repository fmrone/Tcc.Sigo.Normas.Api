using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Api.Dtos;
using Tcc.Sigo.Normas.Api.Models;
using Tcc.Sigo.Normas.Domain.Models;
using Tcc.Sigo.Normas.Domain.Services;

namespace Tcc.Sigo.Normas.Api.Controllers
{
    [Route("normas")]
    [ApiController]
    public class NormaController : ControllerBase
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


    }
}
