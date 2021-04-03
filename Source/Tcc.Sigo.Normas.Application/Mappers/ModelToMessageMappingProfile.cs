using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tcc.Sigo.Normas.Domain.Messages;
using Tcc.Sigo.Normas.Domain.Models;

namespace Tcc.Sigo.Normas.Application.Mappers
{
    public class ModelToMessageMappingProfile : Profile
    {
        public ModelToMessageMappingProfile()
        {
            CreateMap<NormaModel, NormaMessage>();
        }
    }
}
