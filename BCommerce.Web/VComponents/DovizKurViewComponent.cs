using BCommerce.Application.Services;
using BCommerce.Core.DTO;
using BCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;

namespace BCommerce.Web.VComponents
{
    public class DovizKurViewComponent : ViewComponent
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public DovizKurViewComponent(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
        {
            DovuzKurModel model = new DovuzKurModel();

            var client = new RestClient("https://api.collectapi.com/economy/currencyToAll?int=10&base=TRY", configureSerialization: s => s.UseNewtonsoftJson());
            var request = new RestRequest();
            request.Method = Method.Get;


            request.AddHeader("authorization", "apikey 360of1CE50nQ8sYdc2h8iq:7g97DOhjHCXsvFKOb5cYlR");
            request.AddHeader("content-type", "application/json");
            RestResponse<DovuzKurModel> response = client.Execute<DovuzKurModel>(request);


            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {

                string jsonPath = _webHostEnvironment.WebRootPath + "//Dovuz.json";

                string jsonData = await File.ReadAllTextAsync(jsonPath);
                model = JsonConvert.DeserializeObject<DovuzKurModel>(jsonData);


            }
            else
            {
                model = response.Data;
            }

            return View("/Views/Shared/_PartialDovizKur.cshtml", model);

        }


    }

}