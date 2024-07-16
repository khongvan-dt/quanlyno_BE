using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using ZXing;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScanInformationController : ControllerBase
    {
        // POST: api/ScanInformation
        [HttpPost]
        public IActionResult Post([FromBody] List<string> imagePaths)
        {
            try
            {
                if (imagePaths == null || imagePaths.Count == 0)
                {
                    return BadRequest("No image paths provided.");
                }

                var barcodeReader = new BarcodeReader();
                List<string> decodedResults = new List<string>();

                foreach (var imagePath in imagePaths)
                {
                    // Decode QR code from each image path
                    Result result = barcodeReader.Decode(new Bitmap(imagePath));

                    if (result != null)
                    {
                        decodedResults.Add(result.Text);
                    }
                    else
                    {
                        decodedResults.Add("QR Code could not be decoded.");
                    }
                }

                return Ok(decodedResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
