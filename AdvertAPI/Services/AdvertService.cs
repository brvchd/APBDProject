using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;
using AdvertAPI.Exceptions;
using AdvertAPI.Generator;
using AdvertAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdvertAPI.Services
{
    public class AdvertService : IAdvertService
    {
        private readonly masterContext _context;
        private readonly IConfiguration _configuration;
        public AdvertService(masterContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest request)
        {
            var checkUser = _context.Client.Any(p => p.Login.Equals(request.Login) || p.Email.Equals(request.Email));
            if (checkUser) throw new UserExistsException();
            if (!IsValidMail(request.Email)) throw new InvalidMail();

            var salt = SaltGenerator.GenerateSalt();
            var hashedPass = HashPassword.HashPass(request.Password, salt);
            var requestId = GenerateId();

            var client = new Client()
            {
                IdClient = requestId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Login = request.Login,
                Password = hashedPass,
                Salt = salt
            };
            
            var userclaim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(client.IdClient)),
                new Claim(ClaimTypes.Name, request.FirstName),
                new Claim(ClaimTypes.Role, "Customer")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AdCompany",
                audience: "Client",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            client.RefreshToken = Guid.NewGuid().ToString();
            client.ExpireDate = DateTime.Now.AddDays(1);

            _context.Client.Add(client);
            _context.SaveChanges();

            return new RegisterUserResponse { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = client.RefreshToken  };
        }



        public RefreshTokenResponse RefreshToken(string refreshToken)
        {
            var client = _context.Client.FirstOrDefault(p => p.RefreshToken == refreshToken);
            if (client == null) throw new RefreshTokenNotFound();
            if (client.ExpireDate < DateTime.Now) throw new RefreshTokenHasExpired();

            var userclaim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, client.IdClient.ToString()),
                new Claim(ClaimTypes.Name, client.FirstName),
                new Claim(ClaimTypes.Role, "Client"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AdCompany",
                audience: "Client",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            client.RefreshToken = Guid.NewGuid().ToString();
            client.ExpireDate = DateTime.Now.AddDays(1);

            _context.Client.Add(client);
            _context.SaveChanges();

            return new RefreshTokenResponse() { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = client.RefreshToken };

        }




        private int GenerateId()
        {
            return _context.Client.Max(p => p.IdClient) + 1;
        }
        private bool IsValidMail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
