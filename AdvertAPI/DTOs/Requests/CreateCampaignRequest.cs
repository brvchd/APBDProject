using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.DTOs.Requests
{
    public class CreateCampaignRequest
    {
        [Required(ErrorMessage = "Provide client id")]
        public int IdClient { get; set; }
        [Required(ErrorMessage = "Provide start date")]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Provide end date.")]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Provide price per square meter.")]
        public decimal PricePerSquareMeter { get; set; }
        [Required(ErrorMessage = "Provide building ID from which you want to place banners")]
        public int FromIdBuilding { get; set; }
        [Required(ErrorMessage = "Provide building ID to which you want to place banners")]
        public int ToIdBuilding { get; set; }
        [Required(ErrorMessage = "Provide name of banner 1.")]
        public string BannerName1 { get; set; }
        [Required(ErrorMessage = "Provide name of banner 2.")]
        public string BannerName2 { get; set; }
    }
}
