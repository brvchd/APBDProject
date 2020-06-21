using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class CannotFindSuchBuilding : Exception
    {
        public CannotFindSuchBuilding() : base("Provided buildings don't exist")
        {

        }
    }
}
