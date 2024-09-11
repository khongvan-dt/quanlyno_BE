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
    public class RelativeInformationController : ControllerBase
    {
        private readonly RelativeInformationService _relativeInformationService;

        public RelativeInformationController(RelativeInformationService relativeInformationService)
        {
            _relativeInformationService = relativeInformationService;
        }
[HttpPost]
        public IActionResult CreateRelativeInformation([FromBody] RelativeInformation relativeInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return _relativeInformationService.CreateRelativeInformationService(relativeInformation);
        }
        [HttpGet]
        public IActionResult GetRelativeInformation()
        {
            var relativeInformations = _relativeInformationService.GetRelativeInformation();
            return Ok(relativeInformations);
        }

    }
}