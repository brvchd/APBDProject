using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.DTOs.Requests;
using AdvertAPI.Exceptions;
using AdvertAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace AdvertAPI.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IAdvertService _advertService;

        public ClientsController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        [HttpPost]
        public IActionResult RegisterUser(RegisterUserRequest request)
        {
            try
            {
                var response = _advertService.RegisterUser(request);
                return CreatedAtAction("RegisterUser", response);
            }
            catch (UserExistsException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidMail ex1)
            {
                return BadRequest(ex1.Message);
            }
        }


        [HttpPost("{refreshToken}/refresh")]
        public IActionResult RefreshToken([FromRoute] string refreshToken)
        {
            try
            {
                var response = _advertService.RefreshToken(refreshToken);
                return Ok(response);
            }
            catch (RefreshTokenNotFound ex)
            {
                return NotFound(ex.Message);
            }
            catch(RefreshTokenHasExpired ex1)
            {
                return BadRequest(ex1.Message);
            }
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(LoginRequest request)
        {
            try
            {
                var response = _advertService.Login(request);
                return Ok(response);
            }
            catch (UserNotFound ex)
            {
                return NotFound(ex.Message);
            }
        }
            
    }
}
