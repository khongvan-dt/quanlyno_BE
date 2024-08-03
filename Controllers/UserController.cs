using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using quanLyNo_BE.Repository;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class UserController : Repository<User>
    {
        private readonly ApplicationDbContext dc;

        public UserController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
            dc = context;
        }        private readonly IHttpContextAccessor _httpContextAccessor;


        // [HttpPost]
        // [Route("login")]
        // public async Task<IActionResult> Login(User login)
        // {
        //     var user = await dc.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName);
        //     if (user == null)
        //     {
        //         return new  JsonResult(new { message = );
        //     }
        //     if (user.Password == login.Password)
        //     {
        //         int? userId = HttpContext.Session.GetInt32("UserId");
        //         string userName = HttpContext.Session.GetString("UserName");
        //         return new  JsonResult(new { message = user);
        //     }
        //     else
        //     {
        //         return new  JsonResult(new { message = );
        //     }
        // }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User login)
        {
            var user = await dc.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName);

            if (user == null)
            {
                return new  JsonResult(new { message = new { code = 401, msg = "Authentication error: Invalid username or password.", error = "exceptions.UserAuthError" }});
            }

            if (user.Password == login.Password)
            {
                _httpContextAccessor.HttpContext.Session.SetInt32("UserId", user.Id);
                _httpContextAccessor.HttpContext.Session.SetString("UserName", user.UserName);
                return new  JsonResult(new { message = user});
            }
            else
            {
                return new  JsonResult(new { message = new { code = 401, msg = "Authentication error: Invalid username or password.", error = "exceptions.UserAuthError" }});
            }
        }

    }
}