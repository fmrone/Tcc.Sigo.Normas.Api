using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tcc.Sigo.Normas.Api.Dtos;
using Tcc.Sigo.Normas.Domain.Entities;

namespace Tcc.Sigo.Normas.Api.Mappers
{
    public class DtoToEntityMappingProfile : Profile
    {
        /// <summary>
        /// Mapemanto dos dtos para entities - rrite only
        /// </summary>
        public DtoToEntityMappingProfile() 
        {
            CreateMap<NormaPost, NormaEntity>();
        }
    }
}
