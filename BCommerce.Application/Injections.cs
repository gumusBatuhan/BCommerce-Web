using BCommerce.Application.Data;
using BCommerce.Application.Helpers;
using BCommerce.Application.Services;
using BCommerce.Application.Services.AfterRepoAndUOWService;
using BCommerce.Application.UOWAndRepo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application
{
    public static class Injections
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddAutoMapper(typeof(Injections).Assembly);


            services.AddScoped<IdentityService>();
            services.AddScoped<CatalogService>();
            services.AddScoped<BrandService>();
            services.AddScoped<ProductService>();
            services.AddScoped<UserService>();
            services.AddScoped<SpesificationService>();

          
        }
    }
}
