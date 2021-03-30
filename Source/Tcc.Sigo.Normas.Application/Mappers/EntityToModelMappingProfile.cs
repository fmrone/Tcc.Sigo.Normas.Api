using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tcc.Sigo.Normas.Domain.Entities;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Application.Mappers
{
    public class EntityToModelMappingProfile : Profile
    {
        public EntityToModelMappingProfile() 
        {
            CreateMap<NormaEntity, NormaModel>();
        }
    }
}
