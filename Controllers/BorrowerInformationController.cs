using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class BorrowerInformationController : Repository<BorrowerInformation>
    {
        public BorrowerInformationController(ApplicationDbContext dc, IHttpContextAccessor httpContextAccessor) : base(dc, httpContextAccessor)
        {
        }
         
    }
}