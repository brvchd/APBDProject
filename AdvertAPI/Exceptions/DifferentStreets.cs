using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class DifferentStreets : Exception
    {
        public DifferentStreets() : base("Buildings are on different streets.")
        {
                
        }
    }
}
