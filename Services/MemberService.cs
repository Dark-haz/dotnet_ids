using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Models.Entity;
using Microsoft.IdentityModel.Tokens;
using Solution.dotnet_ids.Models.DTO;
using Solution.dotnet_ids.Models.Request;

namespace Solution.dotnet_ids.Services
{
    public class MemberService
    {
        private readonly IRepository<Member> _dbMember;
         private readonly EventService _eventService;
        private readonly IMapper _mapper;
        private readonly string secretKey;

        public MemberService(IRepository<Member> dbMember, IConfiguration configuration, IMapper mapper, EventService eventService)
        {
            _dbMember = dbMember;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret") ?? throw new InvalidOperationException("The JWT secret key cannot be null."); ;
            _eventService = eventService;
        }

        public string GenerateMemberToken(Member member, double minutesExpiry)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name,member.ID.ToString()),
                    new Claim(ClaimTypes.Email,member.Email.ToString()),
                    new Claim(ClaimTypes.Role,"Member"),
                    new Claim(ClaimTypes.DateOfBirth,member.BirthDay.ToString()),
                    new Claim(ClaimTypes.MobilePhone,member.MobileNumber.ToString()),
                    new Claim(ClaimTypes.Gender,member.Gender.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(minutesExpiry),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenhandler.CreateToken(tokenDescriptor);
            var tokenString = tokenhandler.WriteToken(token);

            return tokenString;
        }

        public async Task<LoginResponseDTO> MemberLoginAsync(LoginRequestDTO loginRequest)
        {
            var email = loginRequest.Email;
            var password = loginRequest.Password;

            Member? member = await _dbMember.GetAsync(a => a.Email == email && a.Password == password, false, "Events");

            if (member == null) { throw new Exception("Email or Password is incorrect!");; }

            var user = _mapper.Map<MemberDTO>(member);
            var token = GenerateMemberToken(member, 10);

            return new LoginResponseDTO { User = user, Token = token };
        }

        public async Task<LoginResponseDTO> MemberRegisterAsync(MemberCreateDTO memberCreateDTO)
        {
            var assertEmailExist = await _dbMember.GetAsync(m => m.Email == memberCreateDTO.Email);

            if (assertEmailExist != null) { throw new Exception("Email already exists!"); }

            var member = _mapper.Map<Member>(memberCreateDTO);

            await _dbMember.CreateAsync(member);

            var user = _mapper.Map<MemberDTO>(member);
            var token = GenerateMemberToken(member, 10);

            return new LoginResponseDTO { User = user, Token = token };
        }

        public async Task<Member> GetMemberByIdAsync(int ID , bool tracked , string ?navigation = null)
    {
        Member ? member = await _dbMember.GetAsync(g => g.ID == ID , tracked , navigation) ?? throw new Exception("Member not found!");
        return member;
    }

        public async Task UpdateMemberAsync(Member member)
    {
        await _dbMember.UpdateAsync(member);
    }

    public async Task addEventToMember(int memberID, int eventID)
        {
            Member member = await _dbMember.GetAsync(m => m.ID == memberID, true, "Events") ?? throw new Exception("Member doesn't exist");

            Event e = await _eventService.GetEventByIdAsync(eventID, true);

            if (!member.Events.Contains(e))
            {
                member.Events.Add(e);
            }
            else
            {
                throw new Exception("Member already registered!");
            }

            await _dbMember.UpdateAsync(member);

        }
    }
}