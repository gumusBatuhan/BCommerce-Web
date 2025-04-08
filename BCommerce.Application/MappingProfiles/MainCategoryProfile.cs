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
    public class MainCategoryProfile : Profile
    {
        public MainCategoryProfile()
        {
            CreateMap<MainCategory, MainCategoryDTO>();
        }
    }

    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
