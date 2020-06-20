using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.DTOs.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Provide your login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Provide your password")]
        public string Password { get; set; }
    }
}
