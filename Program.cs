using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using dotnet_ids.Repository;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Data;
using Dotnetids.Models.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Solution.dotnet_ids.Models.DTO;
using Solution.dotnet_ids.Repository.IRepository;
using Solution.dotnet_ids.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    //> Date Only correct swagger config 
    c.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("2023-01-01")
    });

});

//> Register Controllers for swagger (part 1)
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        //> Ignore many to many entities cycles when serializing
        //? Events <-> Members 
        //? get Member with navigational Events --> get Event with navigational Members -> ...
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }); ;

//! My services

//> DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultMysqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultMysqlConnection' not found.")));

//> Repository DI
builder.Services.AddScoped<IRepository<Admin>, AdminRepository>();
builder.Services.AddScoped<IRepository<Member>, MemberRepository>();
builder.Services.AddScoped<IRepository<Guide>, GuideRepository>();
builder.Services.AddScoped<IGuideRepository, GuideRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepository>();

//> Service DI
builder.Services.AddScoped<AdminService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<GuideService>();

//> Bearer Auth
var key = builder.Configuration.GetValue<string>("ApiSettings:Secret") ?? throw new InvalidOperationException("The JWT secret key cannot be null.");

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
).AddJwtBearer(
    options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }
);

//> Automapper
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<Admin, AdminDTO>();
    config.CreateMap<AdminCreateDTO, Admin>();
    config.CreateMap<AdminUpdateDTO, Admin>()
            .ForMember(dest => dest.Password, opt => opt.Condition(src => !string.IsNullOrEmpty(src.Password)));

    config.CreateMap<Member, MemberDTO>();
    config.CreateMap<MemberCreateDTO, Member>();

    config.CreateMap<Event, EventDTO>();

    config.CreateMap<EventCreateDTO, Event>().ReverseMap();
    config.CreateMap<EventUpdateDTO, Event>();

    config.CreateMap<Guide , GuideDTO>().ReverseMap();
    config.CreateMap<GuideUpdateDTO, Guide>().ReverseMap();
    config.CreateMap<GuideCreateDTO, Guide>().ReverseMap();
});

//> CORS for external api access
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500") // Allow your front-end origin
                   .AllowAnyHeader()  // Allow any headers
                   .AllowAnyMethod(); // Allow any HTTP methods (GET, POST, etc.)
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowLocalhost");

//> AFTER CORS
// Add Authentication
app.UseAuthentication();

// Add Authorization
app.UseAuthorization();

//> Register Controllers for swagger (part 1)
app.MapControllers();

app.Run();


