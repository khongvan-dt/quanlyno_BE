using quanLyNo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class RelativeInformationController : Repository<RelativeInformation>
    {
        public RelativeInformationController(ApplicationDbContext dc) : base(dc)
        {
        }
         
    }
}