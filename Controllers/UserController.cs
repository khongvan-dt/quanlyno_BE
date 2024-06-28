using quanLyNo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace quanLyNo.Controllers
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
        
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User login)
        {
            var user = await dc.Users.SingleOrDefaultAsync(u => u.UserName == login.UserName);
            if (user == null)
            {
                return Unauthorized();
            }
            if (user.Password == login.Password)
            {
                int? userId = HttpContext.Session.GetInt32("UserId");
                string userName = HttpContext.Session.GetString("UserName");
                return Ok(user);
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}