using AutoMapper;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDTO>().ForMember(t => t.Items, t2 => t2.MapFrom(src => src.BasketItems));
        }
    }

    public class BasketItemProfile : Profile
    {
        public BasketItemProfile()
        {

            //     CreateMap<User, UserViewModel>()
            //.ForMember(dest =>
            //    dest.FName,
            //    opt => opt.MapFrom(src => src.FirstName))
            //.ForMember(dest =>
            //    dest.LName,
            //    opt => opt.MapFrom(src => src.LastName))

            CreateMap<BasketItem, BasketItemDTO>().ForMember(t => t.Price, t2 => t2.MapFrom(src => src.Product.Price)).ForMember(t => t.Name, t2 => t2.MapFrom(src => src.Product.Name)).ForMember(t => t.Image, t2 => t2.MapFrom(src => src.Product.Images.First().Path));


        }


    }
}
