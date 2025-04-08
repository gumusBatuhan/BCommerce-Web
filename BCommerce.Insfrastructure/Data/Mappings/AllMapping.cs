using BCommerce.Core.DomainClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(100);
        }
    }

    public class MainCategoryMapping : IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.Image).HasMaxLength(100);
        }
    }

    public class BrandMapping : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.Image).HasMaxLength(100);
        }
    }


    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(100);
            builder.Property(t => t.Description).HasMaxLength(4000);
        }
    }



    public class ProductImageMapping : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(t => t.Path).HasMaxLength(100);
        }
    }


    public class SpesificationMapping : IEntityTypeConfiguration<Spesification>
    {
        public void Configure(EntityTypeBuilder<Spesification> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(100);
        }


    }

    public class SpesificationItemMapping : IEntityTypeConfiguration<SpesificationItem>
    {
        public void Configure(EntityTypeBuilder<SpesificationItem> builder)
        {
            builder.Property(t => t.Item).HasMaxLength(100);
        }
    }


    public class ResourceMapping : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.Property(t => t.LanguageKey).HasMaxLength(5);
            builder.Property(t => t.Key).HasMaxLength(100);
            builder.Property(t => t.Value).HasMaxLength(4000);


            //LanguageKey VE Key Datası tekrar etmesin
            builder.HasIndex("LanguageKey", "Key").IsUnique();

        }
    }

}
