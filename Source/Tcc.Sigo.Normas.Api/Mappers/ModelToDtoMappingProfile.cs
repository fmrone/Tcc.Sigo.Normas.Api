using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Api.Dtos;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Api.Mappers
{
    public class ModelToDtoMappingProfile : Profile
    {
        /// <summary>
        /// Mapemanto dos models dtos - retornos read only
        /// </summary>
        public ModelToDtoMappingProfile()
        {
            CreateMap<NormaModel, NormaGet>();
        }
    }
}
