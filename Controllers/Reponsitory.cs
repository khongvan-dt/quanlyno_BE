// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.IO;
// using System.Linq;
// using System.Security.Claims;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using quanLyNo_BE.Common;
// using quanLyNo_BE.Models;

// namespace quanLyNo_BE.Controllers
// {
//     public class Repository<T> : IRepository<T>
//         where T : class
//     {
//         protected readonly ApplicationDbContext _context;
//         private readonly IHttpContextAccessor _httpContextAccessor;

//         public  Repository(ApplicationDbContext dc2, IHttpContextAccessor httpContextAccessor)
//         {
//             dc = dc2;
//             _httpContextAccessor = httpContextAccessor;

//         }

//         // Phương thức hỗ trợ kiểm tra UserId của đối tượng và UserId từ token có khớp nhau hay không
//         private bool IsUserIdMatch(T entity, string userIdFromToken)
//         {
//             var userIdProperty = typeof(T).GetProperty("UserId");
//             if (userIdProperty != null)
//             {
//                 var userIdValue = userIdProperty.GetValue(entity)?.ToString();
//                 return userIdValue == userIdFromToken;
//             }
//             return false; // Nếu không có UserId hoặc không khớp, trả về false
//         }

//         public  IEnumerable<T> GetAll()
//         {
//             var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
//             if (string.IsNullOrEmpty(token))
//             {
//                 // Nếu token không hợp lệ, có thể xử lý phù hợp tại đây 
//                 throw new UnauthorizedAccessException(Constants.Message.TokenMissing);
//             }

//             var handler = new JwtSecurityTokenHandler();
//             var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
//             if (jwtToken == null)
//             {
//                 throw new UnauthorizedAccessException(Constants.Message.InvalidToken);
//             }

//             var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
//             if (userIdClaim == null)
//             {
//                 throw new UnauthorizedAccessException(Constants.Message.UserIdFoundInToken);
//             }

//             var userId = userIdClaim.Value;
//             Console.WriteLine("UserId from token: " + userId);

//             // Lấy danh sách các đối tượng từ cơ sở dữ liệu chỉ khi UserId trùng khớp với UserId trong token
//             var entities = dc.Set<T>().ToList();
//             var filteredEntities = entities.Where(e => IsUserIdMatch(e, userId)).ToList();

//             return filteredEntities;
//         }

//         public  void Create(T value)
//         {

//             var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""); // cho nay khong check null??????

//             Console.WriteLine("Du lieu: {0}", value);
//             Console.WriteLine("Headers: {0}", token); 
//             if (string.IsNullOrEmpty(token))
//             {
//                 return new JsonResult(new { message = Constants.Message.TokenMissing });
//             }

//             var handler = new JwtSecurityTokenHandler();
//             var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
//             if (jwtToken == null)
//                 return new JsonResult(new { message = Constants.Message.InvalidToken });

//             var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

//             if (userIdClaim == null)
//                 return new JsonResult(new { message = Constants.Message.UserIdFoundInToken });


//             var userId = userIdClaim.Value;
//             Console.WriteLine("UserId insert: " + userId);

//             // Kiểm tra UserId không được null hoặc rỗng trước khi thêm vào cơ sở dữ liệu
//             if (string.IsNullOrEmpty(userId))
//             {
//                 return new JsonResult(new { message = Constants.Message.UserIdEmpty });
//             }

//             foreach (var prop in typeof(T).GetProperties())
//             {
//                 if (prop.PropertyType == typeof(DateTime?) && prop.GetValue(value) == null)
//                 {
//                     prop.SetValue(value, DateTime.Now);
//                 }
//             }

//             try
//             {
//                 typeof(T).GetProperty("UserId").SetValue(value, userId);

//                 dc.Set<T>().Add(value);
//                 var save = dc.SaveChanges();
//                 if (save > 0)
//                 {
//                     Console.WriteLine(Constants.Message.CreatedSuccessfully + value.ToString());
//                     return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });
//                 }
//                 else
//                 {

//                     Console.WriteLine(Constants.Message.Createfailure);
//                     foreach (var prop in typeof(T).GetProperties())
//                     {
//                         Console.WriteLine(prop.Name + ": " + prop.GetValue(value));
//                     }
//                     return new JsonResult(new { message = Constants.Message.Createfailure });
//                 }
//             }
//             catch (Exception ex)
//             {
//                 return new JsonResult(new { message = Constants.Message.Createfailure + ex.Message });
//             }
//         }

//         public  T GetById(int id)
//         {
//             return dc.Set<T>().Find(id);
//         }

    
//         public  void Delete(int id)
//         {
//             var item = dc.Set<T>().Find(id);
//             dc.Set<T>().Remove(item);
//             var save = dc.SaveChanges();
//             if (save > 0)
//             {
//                 return new JsonResult(new { message = Constants.Message.DeletedSuccessfully });
//             }
//             return new JsonResult(new { message = Constants.Message.DeletedFailure });
//         }



    
//         public  void UploadImage(IFormFile file)
//         {
//             try
//             {
//                 if (file != null && file.Length > 0)
//                 {
//                     var uploadsFolder = Path.Combine(
//                         Directory.GetCurrentDirectory(),
//                         "wwwroot",
//                         "uploads"
//                     );
//                     if (!Directory.Exists(uploadsFolder))
//                     {
//                         Directory.CreateDirectory(uploadsFolder);
//                     }

//                     var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
//                     var filePath = Path.Combine(uploadsFolder, uniqueFileName);

//                     using (var stream = new FileStream(filePath, FileMode.Create))
//                     {
//                         file.CopyTo(stream);
//                     }
//                     // In ra đường dẫn của ảnh
//                     var imageUrl = Path.Combine("/uploads", uniqueFileName);
//                     // Console.WriteLine("Đường dẫn ảnh: " + imageUrl);
//                     return new JsonResult(new { message = new { imagePath = "/uploads/" + uniqueFileName } });
//                 }
//                 else
//                 {
//                     return new JsonResult(new { message = Constants.Message.NoFileUploaded });
//                 }
//             }
//             catch (Exception ex)
//             {
//                 return new JsonResult(new { message = StatusCodes.Status500InternalServerError, ex.Message });
//             }
//         }

        
//         public  void Update(T value)
//         {
//             dc.Set<T>().Update(value);
//             var save = dc.SaveChanges();
//             if (save > 0)
//             {
//                 return new JsonResult(new { message = Constants.Message.ValueInsertedSuccessfully });
//             }
//             return new JsonResult(new { message = Constants.Message.InsertFAil });
//         }


//     }
// }
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Models;

namespace quanLyNo_BE.Controllers
{
    public class Repository<T> : IRepository<T>
        where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Repository(ApplicationDbContext dc2, IHttpContextAccessor httpContextAccessor)
        {
            _context = dc2;
            _httpContextAccessor = httpContextAccessor;
        }

        private bool IsUserIdMatch(T entity, string userIdFromToken)
        {
            var userIdProperty = typeof(T).GetProperty("UserId");
            if (userIdProperty != null)
            {
                var userIdValue = userIdProperty.GetValue(entity)?.ToString();
                return userIdValue == userIdFromToken;
            }
            return false;
        }

        public IEnumerable<T> Index()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (string.IsNullOrEmpty(token))
            {
                throw new UnauthorizedAccessException(Constants.Message.TokenMissing);
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new UnauthorizedAccessException(Constants.Message.InvalidToken);
            }

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException(Constants.Message.UserIdFoundInToken);
            }

            var userId = userIdClaim.Value;
            Console.WriteLine("UserId from token: " + userId);

            var entities = _context.Set<T>().ToList();
            var filteredEntities = entities.Where(e => IsUserIdMatch(e, userId)).ToList();

            return filteredEntities;
        }

        public IActionResult Create(T value)
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            Console.WriteLine("Du lieu: {0}", value);
            Console.WriteLine("Headers: {0}", token);
            if (string.IsNullOrEmpty(token))
            {
                return new JsonResult(new { message = Constants.Message.TokenMissing });
            }

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
            if (jwtToken == null)
                return new JsonResult(new { message = Constants.Message.InvalidToken });

            var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return new JsonResult(new { message = Constants.Message.UserIdFoundInToken });

            var userId = userIdClaim.Value;
            Console.WriteLine("UserId insert: " + userId);

            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }

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

                _context.Set<T>().Add(value);
                var save = _context.SaveChanges();
                if (save > 0)
                {
                    Console.WriteLine(Constants.Message.CreatedSuccessfully + value.ToString());
                    return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });
                }
                else
                {
                    Console.WriteLine(Constants.Message.Createfailure);
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        Console.WriteLine(prop.Name + ": " + prop.GetValue(value));
                    }
                    return new JsonResult(new { message = Constants.Message.Createfailure });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = Constants.Message.Createfailure + ex.Message });
            }
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public IActionResult Delete(int id)
        {
            var item = _context.Set<T>().Find(id);
            if (item == null)
            {
                return new JsonResult(new { message = Constants.Message.DeletedFailure });
            }

            _context.Set<T>().Remove(item);
            var save = _context.SaveChanges();
            if (save > 0)
            {
                return new JsonResult(new { message = Constants.Message.DeletedSuccessfully });
            }
            return new JsonResult(new { message = Constants.Message.DeletedFailure });
        }

        public IActionResult UploadImage(IFormFile file)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
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

                    var imageUrl = Path.Combine("/uploads", uniqueFileName);
                    return new JsonResult(new { message = new { imagePath = "/uploads/" + uniqueFileName } });
                }
                else
                {
                    return new JsonResult(new { message = Constants.Message.NoFileUploaded });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = StatusCodes.Status500InternalServerError, ex.Message });
            }
        }

        public IActionResult Update(T value)
        {
            _context.Set<T>().Update(value);
            var save = _context.SaveChanges();
            if (save > 0)
            {
                return new JsonResult(new { message = Constants.Message.ValueInsertedSuccessfully });
            }
            return new JsonResult(new { message = Constants.Message.InsertFAil });
        }
    }
}
