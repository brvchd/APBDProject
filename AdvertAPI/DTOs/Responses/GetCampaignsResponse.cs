using AdvertAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.DTOs.Responses
{
    public class GetCampaignsResponse
    {
        public int IdCampaign { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerSquareMeter { get; set; }
        public int FromIdBuilding { get; set; }
        public int ToIdBuilding { get; set; }
        public Customer Customer { get; set; }
        public List<Banner> Banners { get; set; }

    }
}
