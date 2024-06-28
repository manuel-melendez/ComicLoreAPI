using ComicLoreApi.Entities;
using ComicLoreApi.Models;

namespace ComicLoreApi.Services
{
    public interface IUserService
    {
        Task<User> Register(UserRegisterDto userDto);
        Task<(bool Success, string Token)> Login(UserLoginDto userLoginDto);
    }
}
