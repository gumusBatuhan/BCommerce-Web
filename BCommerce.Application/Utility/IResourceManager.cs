using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Application.Utility
{
    //Applicationın elindeki dil ve resource ile metnı cekmesi için gerekli methodları içerir.
    // Applicationın hıc bır zaman baglılık yaratan yapıları kendisi programlamıyor.
    // interface ile ihtiyac listesini cıkarır Insfrastructure bagımlılıkların yer aldıgı yerdır.
    public interface IResourceManager
    {
        string GetResourceByName(string languageKey, string resourceName);
    }
}
