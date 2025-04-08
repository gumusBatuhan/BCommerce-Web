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
    public class BrandProfile: Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandDTO>();
        }
    }
}
