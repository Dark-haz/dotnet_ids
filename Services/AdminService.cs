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
    // Service is contextual to what can happen the app both validation and logic
    public class AdminService
    {
        private readonly IRepository<Admin> _dbAdmin;
        private readonly IMapper _mapper;
        private readonly string secretKey;
        private readonly EventService _eventService;
        private readonly GuideService _guideService;
        private readonly MemberService _memberService;

        public AdminService(IRepository<Admin> dbAdmin, IConfiguration configuration, IMapper mapper, EventService eventService, GuideService guideService, MemberService memberService)
        {
            _dbAdmin = dbAdmin;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret") ?? throw new InvalidOperationException("The JWT secret key cannot be null."); ;
            _eventService = eventService;
            _guideService = guideService;
            _memberService = memberService;
        }

        public string GenerateAdminToken(Admin admin, double minutesExpiry)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,admin.FirstName.ToString()),
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

            Admin? admin = await _dbAdmin.GetAsync(a => a.Email == email && a.Password == password, false);

            if (admin == null) { throw new Exception("Email or Password is incorrect!"); }

            var user = _mapper.Map<AdminDTO>(admin);
            var token = GenerateAdminToken(admin, 120);

            return new LoginResponseDTO { User = user, Token = token };
        }

        public async Task addEventToGuide(int guideID, int eventID)
        {
            Guide guide = await _guideService.GetGuideByIdAsync(guideID, true, "Events");
            Event e = await _eventService.GetEventByIdAsync(eventID, true);

            if (!guide.Events.Contains(e))
            {
                guide.Events.Add(e);
            }
            else
            {
                throw new Exception("Associating not found!");
            }

            await _guideService.UpdateGuideAsync(guide);

        }

        public async Task removeEventFromGuide(int guideID, int eventID)
        {
            Guide guide = await _guideService.GetGuideByIdAsync(guideID, true, "Events");
            Event e = await _eventService.GetEventByIdAsync(eventID, true);

            if (guide.Events.Contains(e))
            {
                guide.Events.Remove(e);
            }
            else
            {
                throw new Exception("Associating not found!");
            }
            await _guideService.UpdateGuideAsync(guide);
        }

        public async Task removeEventFromMember(int memberID, int eventID)
        {
            Member member = await _memberService.GetMemberByIdAsync(memberID, true, "Events");
            Event e = await _eventService.GetEventByIdAsync(eventID, true);

            if (member.Events.Contains(e))
            {
                member.Events.Remove(e);
            }
            else
            {
                throw new Exception("Associating not found!");
            }
            await _memberService.UpdateMemberAsync(member);
        }

        //> CRUD for other entities
        //? Do I implement them herre as services and use inside Controller
        //? Or directly implelemt inside controller

        public async Task<Admin> AddAdminAsync(AdminCreateDTO adminCreateDTO)
        {
            var admin = _mapper.Map<Admin>(adminCreateDTO);
            await _dbAdmin.CreateAsync(admin);
            return admin;
        } 
        

        public async Task<Admin> UpdateAdminAsync(int ID, AdminUpdateDTO adminUpdateDTO)
        {
            var admin = await _dbAdmin.GetAsync(e => e.ID == ID) ?? throw new Exception("Admin not found!");
            _mapper.Map(adminUpdateDTO, admin);
            await _dbAdmin.UpdateAsync(admin);
            return admin;
        }

        public async Task<Admin> DeleteAdminAsync(int ID)
        {
            var e = await _dbAdmin.GetAsync(e => e.ID == ID, true) ?? throw new Exception("Admin not found!");
            await _dbAdmin.DeleteAsync(e);
            return e;
        }
    }


}