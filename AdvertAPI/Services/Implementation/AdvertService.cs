using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;
using AdvertAPI.Exceptions;
using AdvertAPI.Generator;
using AdvertAPI.Helpers;
using AdvertAPI.Models;
using AdvertAPI.Services.Interfaces;
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
        private readonly AdvertAPIContext _context;
        private readonly IConfiguration _configuration;
        private readonly ICalculateArea _service;
        public AdvertService(AdvertAPIContext context, ICalculateArea service, IConfiguration configuration)
        {
            _service = service;
            _context = context;
            _configuration = configuration;
        }

        public RegisterUserResponse RegisterUser(RegisterUserRequest request)
        {
            var checkUser = _context.Client.Any(p => p.Login.Equals(request.Login) || p.Email.Equals(request.Email));
            if (checkUser) throw new UserExistsException();
            if (!MailValidator.IsValidMail(request.Email)) throw new InvalidMail();

            var salt = SaltGenerator.GenerateSalt();
            var hashedPass = HashPassword.HashPass(request.Password, salt);

            var client = new Client()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Login = request.Login,
                Password = hashedPass,
                Salt = salt
            };

            var userclaim = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToString(client.IdClient)),
                new Claim(ClaimTypes.Name, request.FirstName),
                new Claim(ClaimTypes.Role, "Client")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "AdCompany",
                audience: "Clients",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            client.RefreshToken = Guid.NewGuid().ToString();
            client.ExpireDate = DateTime.Now.AddDays(1);

            _context.Client.Add(client);
            _context.SaveChanges();

            return new RegisterUserResponse() { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = client.RefreshToken };
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
                audience: "Clients",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            client.RefreshToken = Guid.NewGuid().ToString();
            client.ExpireDate = DateTime.Now.AddDays(1);
            _context.SaveChanges();

            return new RefreshTokenResponse() { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = client.RefreshToken };

        }

        public LoginResponse Login(LoginRequest request)
        {
            var client = _context.Client.FirstOrDefault(p => p.Login == request.Login);
            if (client == null) throw new UserNotFound();

            var passwordToCompare = HashPassword.HashPass(request.Password, client.Salt);
            if (!passwordToCompare.Equals(client.Password)) throw new UserNotFound();

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
                audience: "Clients",
                claims: userclaim,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: creds
                );

            client.RefreshToken = Guid.NewGuid().ToString();
            client.ExpireDate = DateTime.Now.AddDays(1);
            _context.SaveChanges();

            return new LoginResponse() { AccessToken = new JwtSecurityTokenHandler().WriteToken(token), RefreshToken = client.RefreshToken };
        }

        public List<GetCampaignsResponse> GetCampains()
        {
            var campaigns = _context.Campaign.Join(_context.Client, c => c.IdClient, cl => cl.IdClient, (c, cl) => new GetCampaignsResponse()
            {
                IdCampaign = c.IdCampaign,
                Customer = new Customer()
                {
                    FirstName = cl.FirstName,
                    LastName = cl.LastName,
                    Email = cl.Email,
                    Phone = cl.Phone
                },
                Banners = _context.Banner.Where(d => d.IdCampaign == c.IdCampaign).ToList(),
                StartDate = c.StartDate,
                EndDate = c.StartDate,
                PricePerSquareMeter = c.PricePerSquareMeter,
                FromIdBuilding = c.FromIdBuilding,
                ToIdBuilding = c.ToIdBuilding
            }).OrderByDescending(p => p.StartDate).ToList();

            return campaigns;
        }

        public CreateCampaignResponse CreateCampaign(CreateCampaignRequest request)
        {
            var fromBuilding = _context.Building.FirstOrDefault(p => p.IdBuilding == request.FromIdBuilding);
            var toBuilding = _context.Building.FirstOrDefault(p => p.IdBuilding == request.ToIdBuilding);

            if (fromBuilding == null || toBuilding == null) throw new CannotFindSuchBuilding();
            if (!fromBuilding.Street.Equals(toBuilding.Street)) throw new DifferentStreets();
            if (!fromBuilding.City.Equals(toBuilding.City)) throw new BuildingsInDifferentCities();

            var buildings = _context.Building
                .Where(p => p.StreetNumber >= fromBuilding.StreetNumber && p.StreetNumber <= toBuilding.StreetNumber)
                .OrderByDescending(p => p.Height)
                .ToList();
            var highest_1 = buildings.First();
            var highest_2 = buildings.ElementAt(1);
            var highest_1_Count = buildings.Count(p => p.Height == highest_1.Height);

            var areas = _service.CalculateMinimalArea(highest_1, highest_2, buildings, fromBuilding, toBuilding);

            var bannerPrice_1 = areas.Item1 * request.PricePerSquareMeter;
            var bannerPrice_2 = areas.Item2 * request.PricePerSquareMeter;
            var totalPrice = bannerPrice_1 + bannerPrice_2;

            var roundPrice1 = Math.Round(bannerPrice_1, 2);
            var roundPrice2 = Math.Round(bannerPrice_2, 2);
            var roundTotal = Math.Round(totalPrice, 2);
            

            var campaign = new Campaign()
            {
                IdClient = request.IdClient,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,
                FromIdBuilding = request.FromIdBuilding,
                ToIdBuilding = request.ToIdBuilding
            };
            _context.Campaign.Add(campaign);
            _context.SaveChanges();

            var banner1 = new Banner()
            {
                Name = request.BannerName1,
                Price = roundPrice1,
                IdCampaignNavigation = campaign,
                Area = areas.Item1
            };

            var banner2 = new Banner()
            {
                Name = request.BannerName2,
                Price = roundPrice2,
                IdCampaignNavigation = campaign,
                Area = areas.Item2
            };

            List<Banner> banners = new List<Banner>();
            banners.Add(banner1);
            banners.Add(banner2);

            _context.Banner.Add(banner1);
            _context.Banner.Add(banner2);
            _context.SaveChanges(); 


            return new CreateCampaignResponse()
            {
                IdClient = request.IdClient,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PricePerSquareMeter = request.PricePerSquareMeter,
                FromIdBuilding = request.FromIdBuilding,
                ToIdBuilding = request.ToIdBuilding,
                Banners = banners,
                TotalPrice = roundTotal
            };
        }
    }
}

