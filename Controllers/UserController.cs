using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class UserController : Repository<User>
    {
        private readonly ApplicationDbContext dc;

        public UserController(ApplicationDbContext context) : base(context)
        {
            dc = context;
        }

        // [HttpPost]
        // [Route("login")]
        // public async Task<IActionResult> Login(User login)
        // {
        //     var user = await dc.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName);
        //     if (user == null)
        //     {
        //         return Unauthorized();
        //     }
        //     if (user.Password == login.Password)
        //     {
        //         int? userId = HttpContext.Session.GetInt32("UserId");
        //         string userName = HttpContext.Session.GetString("UserName");
        //         return Ok(user);
        //     }
        //     else
        //     {
        //         return Unauthorized();
        //     }
        // }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User login)
        {
            var user = await dc.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName);

            if (user == null)
            {
                return Unauthorized(new { code = 401, msg = "Authentication error: Invalid username or password.", error = "exceptions.UserAuthError" });
            }

            if (user.Password == login.Password)
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.UserName);
                return Ok(user);
            }
            else
            {
                return Unauthorized(new { code = 401, msg = "Authentication error: Invalid username or password.", error = "exceptions.UserAuthError" });
            }
        }

    }
}