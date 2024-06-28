using quanLyNo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace quanLyNo.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanDoneController : Repository<LoanDone>
    {
        public LoanDoneController(ApplicationDbContext dc) : base(dc)
        {
        }
    }
}