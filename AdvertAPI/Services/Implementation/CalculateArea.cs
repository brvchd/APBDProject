using AdvertAPI.Models;
using AdvertAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Services.Implementation
{
    public class CalculateArea : ICalculateArea
    {
        public Tuple<decimal,decimal> CalculateMinimalArea(Building highest_1, Building highest_2, List<Building> buildings, Building fromBuilding, Building toBuilding)
        {
            decimal bannerArea_1 = 0;
            decimal bannerArea_2 = 0;
            if(highest_1.StreetNumber == fromBuilding.StreetNumber || highest_1.StreetNumber == toBuilding.StreetNumber)
            {
                bannerArea_1 = highest_1.Height;
                bannerArea_2 = highest_2.Height * (buildings.Count - 1);
            }
            else if (highest_2.StreetNumber == fromBuilding.StreetNumber)
            {
                bannerArea_1 = highest_2.Height * buildings.Count(p => p.StreetNumber <= highest_1.StreetNumber);
                bannerArea_2 = highest_1.Height * buildings.Count(p => p.StreetNumber > highest_1.StreetNumber); ;
            }
            else
            {
                if (highest_1.StreetNumber < highest_2.StreetNumber)
                {
                    bannerArea_1 = highest_1.Height * buildings.Count(p => p.StreetNumber <= highest_1.StreetNumber);
                    bannerArea_2 = highest_2.Height * buildings.Count(p => p.StreetNumber > highest_1.StreetNumber);
                }
                else
                {
                    bannerArea_1 = highest_2.Height * buildings.Count(p => p.StreetNumber <= highest_2.StreetNumber);
                    bannerArea_1 = highest_2.Height * buildings.Count(p => p.StreetNumber > highest_2.StreetNumber);

                }
            }
            var areas = Tuple.Create(bannerArea_1, bannerArea_2);
            return areas;
        }
    }
}
