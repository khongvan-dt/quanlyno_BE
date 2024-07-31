using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using quanLyNo_BE.Models;
using quanLyNo_BE.Services;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api")]
    public class LoanRepaymentController : ControllerBase
    {
        private readonly LoanRepaymentService _loanRepaymentService;

        public LoanRepaymentController(LoanRepaymentService loanRepaymentService)
        {
            _loanRepaymentService = loanRepaymentService;
        }

        [HttpPost("CreateLoanRepayment")]
        public IActionResult CreateLoanRepayment([FromBody] LoanRepayment loanRepaymentItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = _loanRepaymentService.CreateLoanRepaymentService(loanRepaymentItem);
            return result;
        }
    }
}

