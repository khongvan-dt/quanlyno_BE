using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Models;
using quanLyNo_BE.Services;
using System.Linq; 

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly LoanRepaymentService _loanRepaymentService;

        public LoanRepaymentController(LoanRepaymentService loanRepaymentService)
        {
            _loanRepaymentService = loanRepaymentService;
        }

        [HttpPost]
        public IActionResult CreateLoanRepayment([FromBody] LoanRepayment loanRepaymentItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return _loanRepaymentService.CreateLoanRepaymentService(loanRepaymentItem);
        }

        [HttpGet]
        public IActionResult GetLoanRepayment()
        {
            var result = _loanRepaymentService.GetLoanRepayment();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No data found." });
            }
            return Ok(result);
        }
    }
}
