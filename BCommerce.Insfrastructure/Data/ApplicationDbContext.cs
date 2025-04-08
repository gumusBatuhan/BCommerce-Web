using BCommerce.Application.Data;
using BCommerce.Core.DomainClasses;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {



        }


        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductSpesification> ProductSpesifications { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<Spesification> Spesifications { get; set; }

        public DbSet<SpesificationItem> SpesificationItems { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbContext CurentContext
        {
            get
            {
                return this;
            }
        }

      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Mappings kalosrundeki mappinglerimizin efcore tarafından değerlendirmeye alınması için gereklidir.
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);



            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("TrustServerCertificate=True;Data Source=.;Integrated Security=True;Database=BCommerceDb");



            base.OnConfiguring(optionsBuilder);
        }

        public int Save()
        {
            return this.SaveChanges();
        }

        public Task<int> ChangesAsync(CancellationToken cancellationToken)
        {
            return this.SaveChangesAsync(cancellationToken);
        }



    }
}
