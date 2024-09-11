using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using quanLyNo_BE.Repository;
using quanLyNo_BE.Services;
using System.Linq;
using quanLyNo_BE.Common;


namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanInformationController : ControllerBase
    {
        private readonly LoanInformationService _loanInformationService;
        public LoanInformationController(LoanInformationService loanInformationService)
        {
            _loanInformationService = loanInformationService;
        }
        [HttpPost]
        public IActionResult CreateLoanInformation([FromBody] LoanInformation loanInformation){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            return _loanInformationService.CreateLoanInformationService(loanInformation);
        }
        [HttpGet]
        public IActionResult GetLoanInformation(){
            var result = _loanInformationService.GetLoanÌnormationService();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = Constants.Message.NoDataFound });
            }
            return Ok(result);
        }
    }
}