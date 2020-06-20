using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class UserNotFound : Exception
    {
        public UserNotFound() : base("Login or password is incorrect")
        {

        }
    }
}
