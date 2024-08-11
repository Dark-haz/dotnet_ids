using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using Solution.dotnet_ids.Models.DTO;
using Solution.dotnet_ids.Models.Request;

namespace Solution.dotnet_ids.Services
{
    public class AdminService
    {
        private readonly IRepository<Admin> _dbAdmin;
        private readonly IMapper _mapper;
        private readonly string  secretKey;

        public AdminService(IRepository<Admin> dbAdmin , IConfiguration configuration , IMapper mapper)
        {
            _dbAdmin = dbAdmin;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret")?? throw new InvalidOperationException("The JWT secret key cannot be null.");;
        }

        public string GenerateAdminToken(Admin admin , double minutesExpiry)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,admin.ID.ToString()),
                    new Claim(ClaimTypes.Email,admin.Email.ToString()),
                    new Claim(ClaimTypes.Role,"Admin"),
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutesExpiry),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);
            var tokenString = tokenhandler.WriteToken(token);

            return tokenString;
        }

        public async Task<LoginResponseDTO> AdminLoginAsync(LoginRequestDTO loginRequest)
        {
            var email = loginRequest.Email;
            var password = loginRequest.Password; 

            Admin ? admin = await _dbAdmin.GetAsync(a => a.Email == email && a.Password == password , false);

            if (admin == null) { return new LoginResponseDTO{User = null , Token = ""}; }

            var user = _mapper.Map<AdminDTO>(admin);
            var token = GenerateAdminToken(admin , 10);

            return new LoginResponseDTO{User = user , Token = token};
        }
    }

        

}