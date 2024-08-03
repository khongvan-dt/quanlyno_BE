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

// var builder = WebApplication.CreateBuilder(args);
// var configuration = builder.Configuration;

// // Cấu hình DbContext và Identity
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
//         ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."))
// );
// builder.Services.AddIdentity<IdentityUser, IdentityRole>()
//     .AddEntityFrameworkStores<ApplicationDbContext>()
//     .AddDefaultTokenProviders(); 


// // // Cấu hình SwaggerGen  
// // builder.Services.AddSwaggerGen(c =>
// // {
// //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Quan ly no", Version = "v1" });

// //     c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
// //     {
// //         Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
// //         Name = "Authorization",
// //         In = ParameterLocation.Header,
// //         Type = SecuritySchemeType.Http,
// //         Scheme = "bearer",
// //         BearerFormat = "JWT"
// //     });

// //     c.AddSecurityRequirement(new OpenApiSecurityRequirement
// //     {
// //         {
// //             new OpenApiSecurityScheme
// //             {
// //                 Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
// //             },
// //             new string[] { }
// //         }
// //     });
// // });



// // Cấu hình Authentication
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
// })
// .AddJwtBearer(options =>
// {
//     options.SaveToken = true;
//     options.RequireHttpsMetadata = false;
//     options.TokenValidationParameters = new TokenValidationParameters
//     {
//         ValidateIssuer = true,
//         ValidateAudience = true,
//         ValidAudience = configuration["JWT:ValidAudience"],
//         ValidIssuer = configuration["JWT:ValidIssuer"],
//         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
//     };
// });


// // // Cấu hình Middleware
// // if (app.Environment.IsDevelopment())
// // {
// //     app.UseDeveloperExceptionPage();
// // }

// // app.UseSwagger();
// // app.UseSwaggerUI(c =>
// // {
// //     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Quan ly no");
// // });
// var app = builder.Build();

// app.UseRouting();
// app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllers();

// app.Run();




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
builder.Services.AddControllers();
builder.Services.AddScoped<LoanRepaymentService>();
builder.Services.AddScoped<BorrowerInformationService>();

var app = builder.Build();

app.UseRouting();
app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

// Add UseAuthentication and UseAuthorization after UseRouting
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
