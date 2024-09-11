using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using quanLyNo_BE.Models;
using quanLyNo_BE.Services;





var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Configure DbContext and Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."))
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

// Add Authorization services
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();

// Add Controllers services
builder.Services.AddScoped<LoanRepaymentService>();
builder.Services.AddScoped<BorrowerInformationService>();
builder.Services.AddScoped<RelativeInformationService>();
builder.Services.AddScoped<LoanInformationService>();
builder.Services.AddScoped<LoanContractService>();

builder.Services.AddControllers();


var app = builder.Build();

app.UseRouting();
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Add UseAuthentication and UseAuthorization after UseRouting
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
