using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using quanLyNo.Common;
using quanLyNo.Models;

namespace quanLyNo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Repository<T> : ControllerBase, IRepository<T>
        where T : class
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        ApplicationDbContext dc;

        public Repository(ApplicationDbContext dc2)
        {
            dc = dc2;
        }

        // [Authorize]
        [HttpGet]
        public IEnumerable<T> Index()
        {
            return dc.Set<T>().ToList();
        }

        [HttpGet("{id}")]
        public T GetById(int id)
        {
            return dc.Set<T>().Find(id);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = dc.Set<T>().Find(id);
            dc.Set<T>().Remove(item);
            var save = dc.SaveChanges();
            if (save > 0)
            {
                return Ok("Xóa Dữ Liệu Thành Công");
            }
            return NotFound("Xóa Thất bại");
        }

        [HttpPost]
        public IActionResult Create([FromBody] T value)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Token is missing");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
                return Unauthorized("Invalid token");

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized("UserId claim not found in token");

            var userId = userIdClaim.Value;
            Console.WriteLine("UserId insert: " + userId);

            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.PropertyType == typeof(DateTime?) && prop.GetValue(value) == null)
                {
                    prop.SetValue(value, DateTime.Now);
                }
            }

            try
            {
                typeof(T).GetProperty("UserId").SetValue(value, userId);

                dc.Set<T>().Add(value);
                var save = dc.SaveChanges();
                if (save > 0)
                {
                    Console.WriteLine("Giá trị được chèn thành công: " + value.ToString());
                    return Ok("Thêm Dữ Liệu Thành Công");
                }
                else
                {
                    Console.WriteLine("Không thể chèn dữ liệu. Dữ liệu bị lỗi:");
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        Console.WriteLine(prop.Name + ": " + prop.GetValue(value));
                    }
                    return NotFound("Thêm Thất bại");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm dữ liệu: " + ex.Message);
                return BadRequest("Lỗi khi thêm dữ liệu: " + ex.Message);
            }
        }

        //
        // [HttpPost]
        // public IActionResult Create([FromBody] T value)
        // {
        //     // Lấy UserId từ Claims của người dùng đăng nhập
        //     var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        //     if (userIdClaim == null)
        //     {
        //         // Xử lý trường hợp không tìm thấy UserId trong Claims
        //         return BadRequest("Không tìm thấy ID người dùng.");
        //     }

        //     var userId = userIdClaim.Value;
        //     Console.WriteLine("UserId insert: " + userId);

        //     // Thiết lập UserId cho đối tượng value
        //     typeof(T).GetProperty("UserId").SetValue(value, userId);

        //     // Kiểm tra và thêm dữ liệu vào cơ sở dữ liệu
        //     try
        //     {
        //         dc.Set<T>().Add(value);
        //         var save = dc.SaveChanges();
        //         if (save > 0)
        //         {
        //             Console.WriteLine("Giá trị được chèn thành công: " + value.ToString());
        //             return Ok("Thêm Dữ Liệu Thành Công");
        //         }
        //         else
        //         {
        //             Console.WriteLine("Không thể chèn dữ liệu. Dữ liệu bị lỗi:");
        //             foreach (var prop in typeof(T).GetProperties())
        //             {
        //                 Console.WriteLine(prop.Name + ": " + prop.GetValue(value));
        //             }
        //             return NotFound("Thêm Thất bại");
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine(Common.Constants.Message.InsertFAil);
        //         Console.WriteLine(ex.Message);
        //         return BadRequest("Lỗi khi thêm dữ liệu: " + ex.Message);
        //     }
        // }

        [HttpPost("upload")]
        public IActionResult UploadImage([FromForm] IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "uploads"
                    );
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    // In ra đường dẫn của ảnh
                    var imageUrl = Path.Combine("/uploads", uniqueFileName);
                    // Console.WriteLine("Đường dẫn ảnh: " + imageUrl);
                    return Ok(new { imagePath = "/uploads/" + uniqueFileName });
                }
                else
                {
                    return BadRequest(Common.Constants.Message.NoFileUploaded);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Update(T value)
        {
            dc.Set<T>().Update(value);
            var save = dc.SaveChanges();
            if (save > 0)
            {
                return Ok(Common.Constants.Message.ValueInsertedSuccessfully);
            }
            return NotFound(Common.Constants.Message.InsertFAil);
        }


    }
}
