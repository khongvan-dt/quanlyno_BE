using quanLyNo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanInformationController : Repository<LoanInformation>
    {
        public LoanInformationController(ApplicationDbContext dc) : base(dc)
        {
        }
    }
}