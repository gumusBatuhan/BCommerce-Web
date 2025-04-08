using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace BCommerce.Web.Models
{
    public class DovuzKurModel
    {
        public bool success { get; set; }

        public DovuzKurModelResult result { get; set; }
    }

    public class DovuzKurModelResult
    {
        [JsonProperty(PropertyName = "base")]
        public string kurbase { get; set; }

        public string lastupdate { get; set; }

        public List<KurData> data { get; set; }

    }
    public class KurData
    {
        public string code { get; set; }

        public string name { get; set; }

        public decimal rate { get; set; }

        public string calculatedstr { get; set; }

        public decimal  Calculated { get; set; }
    }
}
