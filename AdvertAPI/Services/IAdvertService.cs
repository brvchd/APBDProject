using AdvertAPI.DTOs.Requests;
using AdvertAPI.DTOs.Responses;

namespace AdvertAPI.Services
{
    public interface IAdvertService
    {
        public RegisterUserResponse RegisterUser(RegisterUserRequest request);
        public RefreshTokenResponse RefreshToken(string refreshToken);
    }
}
