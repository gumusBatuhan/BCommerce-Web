using BCommerce.Core.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Data
{
    public interface IApplicationDbContext
    {

        DbSet<Brand> Brands { get; set; }

        DbSet<Category> Categories { get; set; }

        DbSet<Product> Products { get; set; }

        DbSet<ProductImage> ProductImages { get; set; }

        DbSet<ProductSpesification> ProductSpesifications { get; set; }

        DbSet<ProductType> ProductTypes { get; set; }

        DbSet<Spesification> Spesifications { get; set; }

        DbSet<SpesificationItem> SpesificationItems { get; set; }

        DbSet<Resource> Resources { get; set; }

        DbSet<MainCategory> MainCategories { get;set; }

        DbSet<BasketItem> BasketItems { get; set; }

        DbSet<Basket> Baskets { get; set; }

        DbSet<Favorite> Favorites { get; set; }
        int Save();

        DbContext CurentContext { get; }

        Task<int> ChangesAsync(CancellationToken cancellationToken);
    }
}
