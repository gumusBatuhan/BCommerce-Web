using AutoMapper;
using BCommerce.Application.Data;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Services
{
    public class UserService
    {

        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;
        public UserService(IApplicationDbContext applicationDbContext, IMapper mapper)
        {

            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Kullanıcının veri tabanındaki basket bilgisini ceker
        /// </summary>
        /// <param name="userId">Eğer loginse userin idsi </param>
        /// <param name="vistorToken">Login değilse visitoru temsil eden token</param>
        /// <returns></returns>
        private Basket getBasket(string userId, Guid? vistorToken)
        {

            return applicationDbContext.Baskets.Include(t => t.BasketItems).ThenInclude(t => t.Product).ThenInclude(t => t.Images).FirstOrDefault(t => (!string.IsNullOrEmpty(userId) && t.UserId == userId) || (vistorToken != null && t.VisitorToken == vistorToken));

        }

        public Result<UserContext> GetUserContext(string userId, Guid? vistorToken)
        {

            var basketItemCount = applicationDbContext.BasketItems.Count(t => (!string.IsNullOrEmpty(userId) && t.Basket.UserId == userId) || (vistorToken != null && t.Basket.VisitorToken == vistorToken));



            Result<UserContext> result = new Result<UserContext>();
            result.IsSuccess = true;
            result.Data = new UserContext();
            result.Data.BasketItemCount = basketItemCount;

            return result;
        }


        public Result AddItem(string userId, Guid? vistorToken, int productid, int count)
        {

            //veri tabanından kontrol et 
            var basket = getBasket(userId, vistorToken);

            //bu kişiye yada visitora ait seper db de yok
            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = userId,
                    VisitorToken = vistorToken
                };
                basket.BasketItems = new List<BasketItem>();
                //dbye bu kisi için sepet ekle
                applicationDbContext.Baskets.Add(basket);
            }


            var basketItemSameProduct = basket.BasketItems.FirstOrDefault(t => t.ProductId == productid);

            if (basketItemSameProduct == null)
            {

                basket.BasketItems.Add(new BasketItem
                {
                    ProductId = productid,
                    Count = count
                });
            }
            else
            {
                basketItemSameProduct.Count += count;
            }


            applicationDbContext.Save();

            return Result.Success();



        }

        public Result UpdateItemCount(string userId, Guid? vistorToken, int itemid, int newCount)
        {
            //ilgili kişi yada visitorun sepetini al
            var basket = getBasket(userId, vistorToken);

            //eğer sepet yoksa hata fırlat
            if (basket == null)
            {
                throw new Exception("Sepet bulunamadı!!");
            }

            //sayısı guncellenecek basketitemi al
            var relatedItem = basket.BasketItems.FirstOrDefault(t => t.Id == itemid);

            //eğer yoksa hata fırlat
            if (relatedItem == null)
            {
                throw new Exception("Sepet itemi bulunamadı!!");

            }

            //eğer yeni count 0 ise itemi sil değilse sayısını güncelle
            if (newCount == 0)
                applicationDbContext.BasketItems.Remove(relatedItem);
            else
                relatedItem.Count = newCount;


            applicationDbContext.Save();

            return Result.Success();
        }

        public Result<BasketDTO> GetUserBasket(string userId, Guid? vistorToken)
        {
            var basket = getBasket(userId, vistorToken);

            if (basket == null)
            {
                basket = new Basket
                {
                    BasketItems = new List<BasketItem>(),
                    UserId = userId,
                    VisitorToken = vistorToken
                };
            }


            var data = mapper.Map<BasketDTO>(basket);
            data.Total = data.Items.Sum(t => t.Price * t.Count);

            Result<BasketDTO> result = new Result<BasketDTO>();
            result.IsSuccess = true;
            result.Data = data;
            return result;


        }

        public Result MergeVistorBasketToUser(string userId, Guid? vistorToken)
        {
            var basket = getBasket(null, vistorToken);

            //sepetinde ürün oldugundan emin olduk
            if (basket != null && basket.BasketItems != null && basket.BasketItems.Count > 0)
            {

                var userBasket = getBasket(userId, null);

                if (userBasket == null)
                {

                    basket.VisitorToken = null;
                    basket.UserId = userId;
                }
                else
                {
                    foreach (var visitorBasketItem in basket.BasketItems)
                    {

                        var userBasketItem = userBasket.BasketItems.FirstOrDefault(t => t.ProductId == visitorBasketItem.ProductId);

                        if (userBasketItem != null)
                        {
                            userBasketItem.Count += visitorBasketItem.Count;
                        }
                        else
                        {
                            userBasket.BasketItems.Add(visitorBasketItem);
                        }

                    }

                    applicationDbContext.Baskets.Remove(basket);


                }

                applicationDbContext.Save();
            }

            return Result.Success();

        }


        public Result AddToFavorite(string userid, int productid, bool isadd)
        {

            if (isadd)
            {

                if (applicationDbContext.Favorites.Any(t => t.UserId == userid && t.ProductId == productid))
                {
                    return Result.Error("Zaten ekli");
                }

                applicationDbContext.Favorites.Add(new Favorite
                {
                    ProductId = productid,
                    UserId = userid,
                    CreateDate = DateTime.Now
                });

            }
            else
            {
                var favoriteInDb = applicationDbContext.Favorites.FirstOrDefault(t => t.UserId == userid && t.ProductId == productid);

                if (favoriteInDb == null)
                {
                    return Result.Error("ekli değil");
                }

                applicationDbContext.Favorites.Remove(favoriteInDb);

            }

            applicationDbContext.Save();
            return Result.Success();

        }
    }
}
