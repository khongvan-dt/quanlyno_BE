// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Reflection.Metadata;
// using System.Security.Claims;
// using System.Text;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using Microsoft.IdentityModel.Tokens;
// using quanLyNo_BE.Models;
// using quanLyNo_BE.Common;

// namespace quanLyNo_BE.controller
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class AuthenticateController : ControllerBase
//     {
//         private readonly UserManager<IdentityUser> _userManager;
//         private readonly RoleManager<IdentityRole> _roleManager;
//         private readonly IConfiguration _configuration;

//         public AuthenticateController(
//             UserManager<IdentityUser> userManager,
//             RoleManager<IdentityRole> roleManager,
//             IConfiguration configuration
//         )
//         {
//             _userManager = userManager;
//             _roleManager = roleManager;
//             _configuration = configuration;
//         }


//         [HttpPost]
//         [Route("login")]
//         public async Task<IActionResult> Login([FromBody] LoginModel model)
//         {
//             var user = await _userManager.FindByNameAsync(model.Username);
//             if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//             {
//                 var userRoles = await _userManager.GetRolesAsync(user);

//                 var authClaims = new List<Claim>
//         {
//             new Claim(ClaimTypes.Name, user.UserName),
//             new Claim(ClaimTypes.NameIdentifier, user.Id), // Sử dụng ClaimTypes.NameIdentifier để lưu ID của người dùng
//             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//             new Claim(JwtRegisteredClaimNames.Sub, user.Id) // Thêm userId vào subject của token
//         };

//                 foreach (var userRole in userRoles)
//                 {
//                     authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//                 }

//                 var token = GetToken(authClaims);

//                 // Console log ID của người dùng
//                 Console.WriteLine("User ID: " + user.Id);

//                 // Trả về token trong phản hồi
//                 return Ok(
//                     new
//                     {
//                         Status = "Success",
//                         Message = "User logged in successfully!",
//                         Token = new JwtSecurityTokenHandler().WriteToken(token),
//                         Expiration = token.ValidTo
//                     }
//                 );
//             }
//             return Unauthorized();
//         }

//         [HttpPost]
//         [Route("register")]
//         public async Task<IActionResult> Register([FromBody] RegisterModel model)
//         {
//             var userExists = await _userManager.FindByNameAsync(model.Username);
//             if (userExists != null)
//                 return StatusCode(
//                     StatusCodes.Status500InternalServerError,
//                     new Response { Status = "Error", Message = "User already exists!" }
//                 );

//             IdentityUser user =
//                 new()
//                 {
//                     Email = model.Email,
//                     SecurityStamp = Guid.NewGuid().ToString(),
//                     UserName = model.Username
//                 };
//             var result = await _userManager.CreateAsync(user, model.Password);
//             if (!result.Succeeded)
//                 return StatusCode(
//                     StatusCodes.Status500InternalServerError,
//                     new Response
//                     {
//                         Status = "Error",
//                         Message = "User creation failed! Please check user details and try again."
//                     }
//                 );

//             return Ok(new Response { Status = "Success", Message = "User created successfully!" });
//         }

//         [HttpPost]
//         [Route("register-admin")]
//         public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
//         {
//             var userExists = await _userManager.FindByNameAsync(model.Username);
//             if (userExists != null)
//                 return StatusCode(
//                     StatusCodes.Status500InternalServerError,
//                     new Response { Status = "Error", Message = "User already exists!" }
//                 );

//             IdentityUser user =
//                 new()
//                 {
//                     Email = model.Email,
//                     SecurityStamp = Guid.NewGuid().ToString(),
//                     UserName = model.Username
//                 };
//             var result = await _userManager.CreateAsync(user, model.Password);
//             Console.WriteLine(result);
//             if (!result.Succeeded)
//                 return StatusCode(
//                     StatusCodes.Status500InternalServerError,
//                     new Response
//                     {
//                         Status = "Error",
//                         Message = "User creation failed! Please check user details and try again."
//                     }
//                 );

//             if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
//                 await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//             if (!await _roleManager.RoleExistsAsync(UserRoles.User))
//                 await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

//             if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
//             {
//                 await _userManager.AddToRoleAsync(user, UserRoles.Admin);
//             }
//             if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
//             {
//                 await _userManager.AddToRoleAsync(user, UserRoles.User);
//             }
//             return Ok(new Response { Status = "Success", Message = "User created successfully!" });
//         }

//         private JwtSecurityToken GetToken(List<Claim> authClaims)
//         {
//             var authSigningKey = new SymmetricSecurityKey(
//                 Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
//             );

//             var token = new JwtSecurityToken(
//                 issuer: _configuration["JWT:ValidIssuer"],
//                 audience: _configuration["JWT:ValidAudience"],
//                 expires: DateTime.Now.AddHours(3),
//                 claims: authClaims,
//                 signingCredentials: new SigningCredentials(
//                     authSigningKey,
//                     SecurityAlgorithms.HmacSha256
//                 )
//             );

//             return token;
//         }
//     }
// }


using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using quanLyNo_BE.Models;
using quanLyNo_BE.Common;

namespace quanLyNo_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            ApplicationDbContext context
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var (token, refreshToken) = GetToken(authClaims);

                return Ok(new
                {
                    Status = "Success",
                    Message = "User logged in successfully!",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo
                });
            }

            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User already exists!"
                });

            var user = new IdentityUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                });

            return Ok(new Response
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User already exists!"
                });

            var user = new IdentityUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
                await _userManager.AddToRoleAsync(user, UserRoles.User);

            return Ok(new Response
            {
                Status = "Success",
                Message = "User created successfully!"
            });
        }

        private (JwtSecurityToken, string) GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(
                    authSigningKey,
                    SecurityAlgorithms.HmacSha256
                )
            );
            var refreshToken = Guid.NewGuid().ToString(); // Tạo refresh token mới

            // Lưu refresh token vào cơ sở dữ liệu
            SaveRefreshTokenToDatabase(refreshToken, authClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            return (token, refreshToken);
        }

        private void SaveRefreshTokenToDatabase(string refreshToken, string userId)
        {
            var token = new RefreshToken
            {
                RToken = refreshToken,
                UserId = userId,
                ExpiryDate = DateTime.Now.AddMinutes(1),
                CreatedDate = DateTime.Now
            };
            _context.RefreshTokens.Add(token);
            _context.SaveChanges();
        }

        [HttpPost("refresh-token")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.RefreshToken))
                return BadRequest("Invalid client request");

            var principal = GetPrincipalFromExpiredToken(request.Token);
            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var savedRefreshToken = _context.RefreshTokens
                .FirstOrDefault(t => t.RToken == request.RefreshToken && t.UserId == userId);

            if (savedRefreshToken == null || savedRefreshToken.ExpiryDate <= DateTime.Now)
                return Unauthorized("Invalid or expired refresh token");

            // Xóa refresh token cũ sau khi đã sử dụng
            _context.RefreshTokens.Remove(savedRefreshToken);
            _context.SaveChanges();

            var (newJwtToken, newRefreshToken) = GetToken(principal.Claims.ToList());
            SaveRefreshTokenToDatabase(newRefreshToken, userId);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(newJwtToken),
                RefreshToken = newRefreshToken
            });
        }


        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false // Bỏ qua kiểm tra thời gian sống của token
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

    }
}
