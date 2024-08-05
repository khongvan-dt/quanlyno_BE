using quanLyNo_BE.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Services;
using System.Linq;
using Microsoft.AspNetCore.Http;

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

            return _borrowerInformationService.CreateBorrowerInformationService(borrowerInformation);
        }
        [HttpGet]
        public IActionResult GetBorrowerInformation()
        {
            var result = _borrowerInformationService.GetBorrowerInformationService();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = Constants.Message.NoDataFound });
            }
            return Ok(new { data = result, message = Constants.Message.DataListSusses });
        }
        [HttpGet("{id}")]
        public IActionResult GetBorrowerInformationById(int id)
        {
            var borrowerInformation = _borrowerInformationService.GetById(id);
            if (borrowerInformation == null)
                return NotFound(new { message = Constants.Message.NoDataFound });
            return Ok(new { data = borrowerInformation, message = Constants.Message.NoDataFound });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowerInformationId(int id)
        {
            var borrowerInformation = _borrowerInformationService.DeleteBorrowerInformationService(id);
            if (borrowerInformation == null)
                return NotFound(new { message = Constants.Message.NoDataFound });
            return Ok(new { message = Constants.Message.DeleteAllSuccessfully });
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBorrowerInformation(int id, [FromBody] BorrowerInformation borrowerInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
                
            return _borrowerInformationService.UpdateBorrowerService(id, borrowerInformation);
        }
        [HttpPost("upload")]
        public IActionResult UploadImageBorrowerFile(IFormFile file)
        {
            var uploadImageResult = _borrowerInformationService.UploadImageBorrowerService(file);
            if (uploadImageResult == null)
            {
                return new JsonResult(new { message = Constants.Message.ImageBack });
            }

            return uploadImageResult;
        }
    }
}