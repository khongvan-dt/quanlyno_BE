using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo_BE.Controllers
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