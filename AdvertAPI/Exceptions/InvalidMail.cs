using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.Exceptions
{
    public class InvalidMail : Exception
    {
        public InvalidMail() : base("Provided e-mail address is invalid.")
        {

        }
    }
}
