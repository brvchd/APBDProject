using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdvertAPI.DTOs.Requests
{
    public class RegisterUserRequest
    {
        [Required(ErrorMessage = "Provide your first name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Provide your last name.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Provide your email name.")]
        public string Email { get; set; }
        [RegularExpression(@"^(1-)?\d{3}-\d{3}-\d{3}$")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Provide your login.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Provide your password.")]
        public string Password { get; set; }
    }
}
