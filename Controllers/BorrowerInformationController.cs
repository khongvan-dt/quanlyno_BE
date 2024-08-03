using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Services;
using System.Linq;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class BorrowerInformationController : ControllerBase
    {
        private readonly BorrowerInformationService _borrowerInformationService;

        public BorrowerInformationController(BorrowerInformationService borrowerInformationService)
        {
            _borrowerInformationService = borrowerInformationService;
        }

        [HttpPost]
        public IActionResult CreateBorrowerInformation([FromBody] BorrowerInformation borrowerInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _borrowerInformationService.CreateBorrowerInformation(borrowerInformation);
        }
        [HttpGet]
        public IActionResult GetBorrowerInformation()
        {
            var result = _borrowerInformationService.GetBorrowerInformation();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = Constants.Message.NoDataFound });
            }
            return Ok(new { data=result,message = Constants.Message.DataListSusses });
        }
        [HttpGet("{id}")]
        public IActionResult GetBorrowerInformationById(int id)
        {
            var borrowerInformation = _borrowerInformationService.GetById(id);
            if (borrowerInformation == null)
                return NotFound(new { message = Constants.Message.NoDataFound });
            return Ok(new { data=borrowerInformation, message = Constants.Message.NoDataFound });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowerInformationId(int id)
        {
            var borrowerInformation = _borrowerInformationService.DeleteBorrowerInformation(id);
            if (borrowerInformation == null)
                return NotFound(new { message = Constants.Message.NoDataFound });
            return Ok(new { message = Constants.Message.DeleteAllSuccessfully } );
        }
    }
}