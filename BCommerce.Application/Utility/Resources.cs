using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Utility
{

    // UI  Ve Applicationda  metin ihtiyacı olursa kesinlikle veritabanından almali (Multilanguage bir proje yiz)
    public class Resources
    {

        private static IResourceManager _resourceManager;

        public static void SetManager(IResourceManager resourceManager)
        {
            _resourceManager = resourceManager;
        }

        public static string L(string key)
        {


            string language = "tr-TR";
            return _resourceManager.GetResourceByName(key, language);
        }
    }
}
