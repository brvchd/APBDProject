using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AdvertAPI.Models
{
    public partial class Banner
    {
        public int IdAdvertisement { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int IdCampaign { get; set; }
        public decimal Area { get; set; }

        [JsonIgnore]
        public virtual Campaign IdCampaignNavigation { get; set; }
    }
}
