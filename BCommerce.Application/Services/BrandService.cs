using AutoMapper;
using BCommerce.Application.Data;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Services
{
    public class BrandService
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BrandService(IApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;

        }

        public Result<List<BrandDTO>> GetBrands(int takeCount = 0)
        {

            Result<List<BrandDTO>> result = new Result<List<BrandDTO>>();

            List<Brand> brandsInDb = new List<Brand>();

            if (takeCount > 0)
            {

                brandsInDb = _context.Brands.Take(takeCount).ToList();
            }
            else
                brandsInDb = _context.Brands.ToList();


            var brandDtos = _mapper.Map<List<BrandDTO>>(brandsInDb);

            result.Data = brandDtos;
            result.IsSuccess = true;
            return result;


        }

        public Result<BrandGroupByFirstLetterDTO> GetBrandsWithFirstLetterGrouped()
        {

            var allBrands = GetBrands();


            var newData = (from t in allBrands.Data
                           group t by t.Name.First() into g
                           select new BrandGroupByFirstLetterItemDTO
                           {
                               FirstLetter = g.Key.ToString(),
                               Brands = g.ToList()

                           }).ToList();

            Result<BrandGroupByFirstLetterDTO> result = new Result<BrandGroupByFirstLetterDTO>();
            result.Data = new BrandGroupByFirstLetterDTO();
            result.Data.AllBrands = newData;
            result.IsSuccess = true;
            return result;



        }
    }
}
