using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using quanLyNo_BE.Repository;
using quanLyNo_BE.Services;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanContractController : ControllerBase
    {
        private readonly LoanContractService _loanContractService;

        public LoanContractController(LoanContractService loanContractService)
        {
            _loanContractService = loanContractService;
        }
        [HttpPost]
        public IActionResult CreateLoanContract([FromBody] LoanContract loanContract)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            return _loanContractService.CreateLoanContractService(loanContract);
        }
    }
}