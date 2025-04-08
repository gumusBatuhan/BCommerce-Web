using BCommerce.Web.Helpers.Models;
using BCommerce.Web.Models;
using Microsoft.Extensions.Primitives;

namespace BCommerce.Web.Helpers
{
    public class QueryHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public QueryHelper(IHttpContextAccessor contextAccessor)
        {

            _contextAccessor = contextAccessor;

        }


        public string CreateQueyFromFilterData(FilterAreaModel model)
        {
            string query = "?";

            if (model.MinPrice.HasValue)
            {
                query += $"minprice={model.MinPrice}&&";
            }


            if (model.MaxPrice.HasValue)
            {
                query += $"maxprice={model.MaxPrice}&&";
            }

            if (model.SelectedBrands.Any())
            {
                //1 2 3 -->1,2,3
                query += $"brands={string.Join(",", model.SelectedBrands)}&&";
            }

            if (model.SelectedSpesifications.Any())
            {
                //1 2 3 -->1,2,3
                query += $"spesifications={string.Join(",", model.SelectedSpesifications)}&&";
            }



            return query;
        }


        //?minprice=2000&&maxprice=7200&&brands=2,3
        /// <summary>
        /// Query string
        /// </summary>
        /// <returns></returns>
        public FilterAreaHelperModel CreateFilterModelFromQueryString()
        {

            FilterAreaHelperModel filterAreaHelperModel = new FilterAreaHelperModel();

            var httpRequest = _contextAccessor.HttpContext.Request;

            StringValues minprice;
            httpRequest.Query.TryGetValue("minprice", out minprice);


            if (minprice.Any())
            {
                filterAreaHelperModel.MinPrice = Convert.ToDecimal(minprice.First());
            }


            StringValues maxprice;
            httpRequest.Query.TryGetValue("maxprice", out maxprice);

            if (maxprice.Any())
            {
                filterAreaHelperModel.MaxPrice = Convert.ToDecimal(maxprice.First());
            }

            StringValues brands;
            httpRequest.Query.TryGetValue("brands", out brands);

            //1,2,3
            if (brands.Any())
            {
                string allBrands = brands.First();
                filterAreaHelperModel.SelectedBrandIds = allBrands.Split(",").Select(t => Convert.ToInt32(t)).ToList();

            }



            StringValues spesifications;
            httpRequest.Query.TryGetValue("spesifications", out spesifications);

            if (spesifications.Any())
            {
                string allSpesifications = spesifications.First();
                filterAreaHelperModel.SelectedSpesificationIds = allSpesifications.Split(",").Select(t => Convert.ToInt32(t)).ToList();

            }

            return filterAreaHelperModel;


        }
    }
}
