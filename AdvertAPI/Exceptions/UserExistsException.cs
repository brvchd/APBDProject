using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class UserExistsException : Exception
    {
        public UserExistsException() : base("User already exists")
        {

        }
    }
}
