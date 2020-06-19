using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class RefreshTokenHasExpired : Exception
    {
        public RefreshTokenHasExpired() : base("Refresh token has expired")
        {

        }
    }
}
