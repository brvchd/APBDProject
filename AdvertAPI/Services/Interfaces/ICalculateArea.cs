using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;
using AdvertAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Services.Interfaces
{
    public interface ICalculateArea
    {
        public Tuple<decimal, decimal> CalculateMinimalArea(Building highest_1, Building highest_2, List<Building> buildings, Building fromBuilding, Building toBuilding);
    }
}
