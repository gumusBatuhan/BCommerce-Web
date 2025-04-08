using AutoMapper;
using BCommerce.Application.Data;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Services
{
    public class CatalogService
    {

        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        public CatalogService(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }


        public Result<List<MainCategoryDTO>> GetAllCategory()
        {
            //Veri tabanından veriyi al
            var allCategoryIndB = _applicationDbContext.MainCategories.Include(t => t.Categories).ToList();

            var mainCategoryList= _mapper.Map<List<MainCategoryDTO>>(allCategoryIndB);


            //DTO a maple
            //List<MainCategoryDTO> mainCategoryList=new List<MainCategoryDTO>();
          
            //foreach (var mainCategoryInDb in allCategoryIndB)
            //{
            //    MainCategoryDTO mainCat = new MainCategoryDTO();

            //    mainCat.Name = mainCategoryInDb.Name;
            //    mainCat.Image= mainCategoryInDb.Image;
            //    mainCat.Categories = new List<CategoryDTO>();

            //    foreach (var categoryInDb in mainCategoryInDb.Categories)
            //    {
            //        mainCat.Categories.Add(new CategoryDTO
            //        {
            //            Name = categoryInDb.Name
            //        });
            //    }
            //    mainCategoryList.Add(mainCat);
            //}


            //geri don
            return new Result<List<MainCategoryDTO>>
            {
                Data = mainCategoryList,
                IsSuccess = true
            };

        }


    }
}
