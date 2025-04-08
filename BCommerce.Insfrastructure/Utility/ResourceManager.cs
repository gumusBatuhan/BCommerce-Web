using BCommerce.Application.Data;
using BCommerce.Application.Utility;
using BCommerce.Core.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Insfrastructure.Utility
{
    public class ResourceManager : IResourceManager
    {
        private static List<Resource> resources = new List<Resource>();


        private readonly IApplicationDbContext _applicationDbContext;
        public ResourceManager(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }


        public string GetResourceByName(string languageKey, string resourceName)
        {

            //eğer statıc olarak tuttugum liste bossa ver tabanından tüm dil listesini cek ve buna aktar
            //cache
            if (resources == null)
            {
                resources = _applicationDbContext.Resources.ToList();
            }

            var resource = resources.FirstOrDefault(t => t.LanguageKey == languageKey && t.Key == resourceName);
          

            if (resource == null) {

                return $"{languageKey}-{resourceName} not found";
            }

            return resource.Value;
        
        }
    }
}
