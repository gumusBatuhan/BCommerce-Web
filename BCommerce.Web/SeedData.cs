using BCommerce.Application.Data;
using BCommerce.Core.DomainClasses;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Reflection;
using System.Text.Json.Serialization;

namespace BCommerce.Web
{
    public class SeedData
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IWebHostEnvironment webHost;


        public SeedData(RoleManager<ApplicationRole> roleManager, IApplicationDbContext applicationDbContext, IWebHostEnvironment webHost)
        {
            this.roleManager = roleManager;
            this.applicationDbContext = applicationDbContext;
            this.webHost = webHost;
        }

        public async Task Seed()
        {


            ApplicationRole applicationRole = await roleManager.FindByNameAsync("Admin");

            if (applicationRole == null)
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "Admin"
                });
            }


            ApplicationRole webUserRole = await roleManager.FindByNameAsync("WebUser");

            if (webUserRole == null)
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = "WebUser"
                });
            }


            bool hasLaptopAndTablets = applicationDbContext.MainCategories.Any(t => t.Name == "Laptops & Tablets");

            if (!hasLaptopAndTablets)
            {
                applicationDbContext.MainCategories.Add(new MainCategory
                {
                    Name = "Laptops & Tablets",
                    Image = "/images/catalog/computers.svg",
                    Categories = new List<Category> {

                        new Category
                        {
                            Name="Laptops"
                        },
                        new Category
                        {
                            Name="Tablets"
                        },
                        new Category
                        {
                            Name="Peripherals"
                        },
                        new Category
                        {
                            Name="Keyboards"
                        },
                        new Category
                        {
                            Name="Accessories"
                        }

                    }
                });
            };



            //Laptops & Tablets
            //Laptops 
            //Tablets 
            //Peripherals
            //Keyboards 
            //Accessories


            //Phones & Gadgets

            //Smartphones
            //Mobile Phones
            //Charging and batteries
            //Accessories



            bool hasPhoneAndGadgets = applicationDbContext.MainCategories.Any(t => t.Name == "Phones & Gadgets");

            if (!hasLaptopAndTablets)
            {
                applicationDbContext.MainCategories.Add(new MainCategory
                {
                    Name = "Phones & Gadgets",
                    Image = "/images/catalog/phones.svg",
                    Categories = new List<Category> {

                        new Category
                        {
                            Name="Smartphones"
                        },
                        new Category
                        {
                            Name="Mobile Phones"
                        },
                        new Category
                        {
                            Name="Charging and batteries"
                        },
                        new Category
                        {
                            Name="Accessories"
                        }

                    }
                });
            };


            //TV & Video

            //TV
            //Home Cinema
            //Satellite & amp; Cable TV
            //Projectors
            //DVD & amp; Blu-ray
            //Accessories

            bool hasTvAndVideos = applicationDbContext.MainCategories.Any(t => t.Name == "TV & Video");

            if (!hasLaptopAndTablets)
            {
                applicationDbContext.MainCategories.Add(new MainCategory
                {
                    Name = "TV & Video",
                    Image = "/images/catalog/computers.svg",
                    Categories = new List<Category> {

                        new Category
                        {
                            Name="TV"
                        },
                        new Category
                        {
                            Name="Home Cinema"
                        },
                        new Category
                        {
                            Name="Satellite & Cable TV"
                        },
                        new Category
                        {
                            Name="Projectors"
                        }

                    }
                });
            };


            //brands
            int brandCount = applicationDbContext.Brands.Count();


            if (brandCount == 0)
            {


                string fullPath = this.webHost.WebRootPath + "/images/brands";

                DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);

                foreach (var item in directoryInfo.GetFiles())
                {

                    string brandName = item.Name.Replace(".svg", "");

                    brandName = brandName.First().ToString().ToUpper() + brandName.Substring(1);

                    applicationDbContext.Brands.Add(new Brand { Name = brandName, Image = "/images/brands/" + item.Name });

                }


            }



            //ürünler


            var productCount = applicationDbContext.Products.Count();

            if (productCount == 0)
            {

                var seedProductJsonText = File.ReadAllText(webHost.WebRootPath + "/SeedProduct.json");


                List<Product> productData = JsonConvert.DeserializeObject<List<Product>>(seedProductJsonText);

                applicationDbContext.Products.AddRange(productData);
            }



            await applicationDbContext.ChangesAsync(default);





        }
    }
}
