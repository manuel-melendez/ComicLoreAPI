using ComicLoreApi.DbContexts;
using ComicLoreApi.Entities;
using ComicLoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ComicLoreApi.Services
{
    public class UserService : IUserService
    {
        private readonly SupeInfoDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(SupeInfoDbContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<(bool Success, string Token)> Login(UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userLoginDto.UserName);

            if(user == null || user.PassWord != userLoginDto.PassWord)
            {
                return(false, null);
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["ApplicationSettings:JWT_Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return(true, tokenHandler.WriteToken(token));
        }

        public async Task<User> Register(UserRegisterDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                PassWord = userDto.PassWord,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
