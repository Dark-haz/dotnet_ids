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

public class MemberService
{
    private readonly IRepository<Member> _dbMember;
    private readonly IMapper _mapper;
    private readonly string secretKey;

    public MemberService(IRepository<Member> dbMember, IConfiguration configuration, IMapper mapper)
    {
        _dbMember = dbMember;
        _mapper = mapper;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret") ?? throw new InvalidOperationException("The JWT secret key cannot be null."); ;
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

        if (member == null) { return new LoginResponseDTO { User = null, Token = "" }; }

        var user = _mapper.Map<MemberDTO>(member);
        var token = GenerateMemberToken(member, 10);

        return new LoginResponseDTO { User = user, Token = token };
    }
    
    public async Task<LoginResponseDTO> MemberRegisterAsync(MemberCreateDTO memberCreateDTO)
    {
        var assertEmailExist = await _dbMember.GetAsync(m => m.Email == memberCreateDTO.Email);

        if(assertEmailExist != null){return new LoginResponseDTO { User = null , Token = "" };}

        var member = _mapper.Map<Member>(memberCreateDTO);

        await _dbMember.CreateAsync(member);

        var user = _mapper.Map<MemberDTO>(member);
        var token = GenerateMemberToken(member, 10);

        return new LoginResponseDTO { User = user, Token = token };
    }
}
