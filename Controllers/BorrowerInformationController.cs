using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class BorrowerInformationController : Repository<BorrowerInformation>
    {
        public BorrowerInformationController(ApplicationDbContext dc) : base(dc)
        {
        }
         
    }
}