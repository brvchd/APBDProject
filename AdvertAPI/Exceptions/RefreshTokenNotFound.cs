using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class RefreshTokenNotFound : Exception
    {
        public RefreshTokenNotFound() : base("Cannot find such token")
        {

        }
    }
}
