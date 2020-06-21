using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class BuildingsInDifferentCities : Exception
    {
        public BuildingsInDifferentCities() : base("Buildings are in different cities")
        {

        }
    }
}
