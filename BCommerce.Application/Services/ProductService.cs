using AutoMapper;
using BCommerce.Application.Data;
using BCommerce.Application.UOWAndRepo.Repositories;
using BCommerce.Core.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Services
{
    public class ProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(IApplicationDbContext applicationDbContext, IMapper mapper, IProductRepository productRepository)
        {
            _context = applicationDbContext;
            _mapper = mapper;
            _productRepository=productRepository;

        }

        private void setFavoriteDataToProductDTO(List<ProductDTO> products, string userid)
        {
            if (userid == null)
            {
                return;
            }

            var allFavorites = _context.Favorites.Where(t => t.UserId == userid).ToList();


            foreach (var favoriteItem in allFavorites)
            {
                var favoriteItemInProductDto = products.FirstOrDefault(t => t.Id == favoriteItem.ProductId);

                if (favoriteItemInProductDto != null)
                {
                    favoriteItemInProductDto.IsFavorite = true;

                }
            }

        }

        public Result<List<ProductDTO>> GetTrendProducts(string userid)
        {
            
            var trendProducts = _context.Products.Include(t => t.Images).Include(t => t.Category).Include(t => t.Brand).ToList();

            Result<List<ProductDTO>> result = new Result<List<ProductDTO>>();


            result.Data = _mapper.Map<List<ProductDTO>>(trendProducts);
            setFavoriteDataToProductDTO(result.Data, userid);
            return result;
        }

        public Result<FilteredProductDTO> GetFilteredProducts(string userid,ProductFilterModelDTO productFilterModelDTO)
        {

            Result<FilteredProductDTO> result = new Result<FilteredProductDTO>();
            var products = _productRepository.GetFilteredProducts(productFilterModelDTO);

            result.IsSuccess = true;
            result.Data = new FilteredProductDTO
            {
                Products = _mapper.Map<List<ProductDTO>>(products),
                ProductCount=products.Count,
            };
            setFavoriteDataToProductDTO(result.Data.Products, userid);

            return result;

        }
    }
}
