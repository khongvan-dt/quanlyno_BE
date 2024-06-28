using quanLyNo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo.Controllers
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