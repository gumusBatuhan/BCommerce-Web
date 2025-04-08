using BCommerce.Application.Data;
using BCommerce.Application.Helpers;
using BCommerce.Application.Services;
using BCommerce.Application.UOWAndRepo;
using BCommerce.Application.UOWAndRepo.Repositories;
using BCommerce.Insfrastructure.Data;
using BCommerce.Insfrastructure.Helpers;
using BCommerce.Insfrastructure.UOWAndRepo;
using BCommerce.Insfrastructure.UOWAndRepo.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Extensions
{
    public static class Injections
    {
        public static void AddInfrasture(this IServiceCollection services, IConfiguration configuration)
        {

            var useInMemory = Convert.ToBoolean(configuration["UseInMemoryDb"]);

            services.AddDbContext<ApplicationDbContext>(cong =>
            {
                if (useInMemory)
                {
                    cong.UseInMemoryDatabase("BCommerceDb");

                }
                else
                {
                    cong.UseSqlServer(configuration.GetConnectionString("SqlServer"));
                }

            });
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            services.AddScoped<IIdentityHelper, IdentityHelper>();

            services.AddScoped<IValidationHelper, ValidationHelper>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ISpesificationRepository, SpesificationRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();



        }
    }
}
