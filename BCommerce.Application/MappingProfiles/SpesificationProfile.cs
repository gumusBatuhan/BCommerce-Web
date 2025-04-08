using AutoMapper;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.MappingProfiles
{
    public class SpesificationProfile : Profile
    {
        public SpesificationProfile()
        {
            CreateMap<Spesification, SpesificationDTO>();
        }
    }


    public class SpesificationItemProfile : Profile
    {
        public SpesificationItemProfile()
        {
            CreateMap<SpesificationItem, SpesificationItemDTO>();
        }

    }
}
