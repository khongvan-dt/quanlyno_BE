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
    public class LoanContractController : Repository<LoanContract>
    {
        public LoanContractController(ApplicationDbContext dc,IHttpContextAccessor httpContextAccessor) : base(dc, httpContextAccessor)
        {
        }
    }
}