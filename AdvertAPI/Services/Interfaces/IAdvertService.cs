using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;
using System.Collections.Generic;

namespace AdvertAPI.Services
{
    public interface IAdvertService
    {
        public RegisterUserResponse RegisterUser(RegisterUserRequest request);
        public RefreshTokenResponse RefreshToken(string refreshToken);
        public LoginResponse Login(LoginRequest request);
        public List<GetCampaignsResponse> GetCampains();
        public CreateCampaignResponse CreateCampaign(CreateCampaignRequest request);
    }
}
