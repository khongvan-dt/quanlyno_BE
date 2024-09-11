// using Microsoft.AspNetCore.Mvc;
// using quanLyNo_BE.Services;
// using System;

// namespace quanLyNo_BE.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class CccdController : ControllerBase
//     {
//         private readonly ICccdService _cccdService;

//         public CccdController(ICccdService cccdService)
//         {
//             _cccdService = cccdService;
//         }

//         [HttpGet("generate")]
//         public IActionResult GenerateQRCode(string text)
//         {
//             try
//             {
//                 var qrCodeImage = _cccdService.GenerateQRCodeImage(text);
//                 var base64String = Convert.ToBase64String(qrCodeImage);
//                 return Ok(new { qrCodeImage = $"data:image/png;base64,{base64String}" });
//             }
//             catch (ArgumentException ex)
//             {
//                 return BadRequest(ex.Message);
//             }
//         }

//         [HttpPost("decode")]
//         public IActionResult DecodeQRCode([FromBody] byte[] qrCodeImage)
//         {
//             try
//             {
//                 var text = _cccdService.DecodeQRCode(qrCodeImage);
//                 return Ok(new { text });
//             }
//             catch (ArgumentException ex)
//             {
//                 return BadRequest(ex.Message);
//             }
//             catch (InvalidOperationException ex)
//             {
//                 return NotFound(ex.Message);
//             }
//         }
//     }
// }
