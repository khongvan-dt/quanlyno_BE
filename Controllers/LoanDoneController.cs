using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using quanLyNo_BE.Repository;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanDoneController : Repository<LoanDone>
    {
        public LoanDoneController(ApplicationDbContext dc,IHttpContextAccessor httpContextAccessor) : base(dc,httpContextAccessor)
        {
        }
    }
}