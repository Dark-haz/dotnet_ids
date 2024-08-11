using System.Text;
using dotnet_ids.Repository;
using dotnet_ids.Repository.IRepository;
using Dotnetids.Data;
using Dotnetids.Models.Entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Solution.dotnet_ids.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//! My services

//> DB context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultMysqlConnection") ?? throw new InvalidOperationException("Connection string 'DefaultMysqlConnection' not found.")));

//> Repository DI
builder.Services.AddScoped<IRepository<Admin>, AdminRepository>();
builder.Services.AddScoped<IRepository<Member>, MemberRepository>();
builder.Services.AddScoped<IRepository<Guide>, GuideRepository>();
builder.Services.AddScoped<IRepository<Event>, EventRepository>();



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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();


