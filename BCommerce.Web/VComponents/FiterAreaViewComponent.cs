using BCommerce.Application.Services;
using BCommerce.Application.Services.AfterRepoAndUOWService;
using BCommerce.Core.DomainClasses;
using BCommerce.Web.Helpers;
using BCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.VComponents
{
    public class FiterAreaViewComponent : ViewComponent
    {
        private readonly BrandService brandService;
        private readonly SpesificationService spesificationService;
        private readonly QueryHelper queryHelper;

        public FiterAreaViewComponent(BrandService brandService, QueryHelper queryHelper, SpesificationService spesificationService)
        {
            this.brandService = brandService;
            this.queryHelper = queryHelper;
            this.spesificationService = spesificationService;
        }

        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
        {


            var queryData = queryHelper.CreateFilterModelFromQueryString();


            var allBrandResult = brandService.GetBrands();

            FilterAreaModel filterAreaModel = new FilterAreaModel();

            #region GLOBAL Filtereler
            filterAreaModel.MinPrice = queryData.MinPrice;
            filterAreaModel.MaxPrice = queryData.MaxPrice;

            foreach (var item in allBrandResult.Data)
            {
                FilterBrand brand = new FilterBrand
                {
                    Name = item.Name,
                    Id = item.Id,
                    IsSelected = queryData.SelectedBrandIds.Any(t => t == item.Id)

                };

                filterAreaModel.Brands.Add(brand);
            }

            #endregion


            var posibleSpesificationResult = await this.spesificationService.GetPosibleSpesification();
            filterAreaModel.Spesifications = posibleSpesificationResult.Data;


            foreach (var item in filterAreaModel.Spesifications)
            {
                foreach (var sitem in item.SpesificationItems)
                {
                    sitem.IsSelected = queryData.SelectedSpesificationIds.Any(t => t == sitem.Id);
                }
            }


            return View("/Views/Shared/FilterArea/_PartialFilter.cshtml", filterAreaModel);

        }
    }
}
