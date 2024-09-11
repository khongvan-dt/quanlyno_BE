using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Models;
using System.Collections.Generic;
using System;
using quanLyNo_BE.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
namespace quanLyNo_BE.Services
{
    public class BorrowerInformationService : Repository<BorrowerInformation>
    {
        private readonly ApplicationDbContext dc;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BorrowerInformationService(ApplicationDbContext dc, IHttpContextAccessor httpContextAccessor)
            : base(dc, httpContextAccessor)
        {
            this.dc = dc;
            _httpContextAccessor = httpContextAccessor;

        }
        public IActionResult CreateBorrowerInformationService(BorrowerInformation borrowerInformation)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            typeof(BorrowerInformation).GetProperty("UserId")?.SetValue(borrowerInformation, userId);
            // Nhân các thuộc tính kiểu int với 1
            var properties = typeof(BorrowerInformation).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    var currentValue = (int)property.GetValue(borrowerInformation);
                    property.SetValue(borrowerInformation, currentValue * 1);
                }
            }
            Create(borrowerInformation);
            return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });

        }
        public IEnumerable<BorrowerInformation> GetBorrowerInformationService()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            var borrowerInformationList = base.Index();
            var dataList = borrowerInformationList
                .Where(x => IsUserIdMatch(x, userId)).ToList();
            return dataList;
        }

        public IActionResult DeleteBorrowerInformationService(int id)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            Delete(id);
            return new JsonResult(new { message = Constants.Message.DeletedSuccessfully });
        }
        public IActionResult GetBorrowerIdService(int id)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            var borrowerId = base.GetById(id);
            if (borrowerId == null)
            {
                return new JsonResult(new { message = Constants.Message.NoDataFound });
            }

            return new JsonResult(borrowerId);
        }
        public IActionResult UpdateBorrowerService(int id, BorrowerInformation borrowerInformation)
        {
            // Lấy bản ghi hiện tại từ cơ sở dữ liệu
            var currentData = base.GetById(id);
            if (currentData == null)
            {
                return new JsonResult(new { message = Constants.Message.NoDataFound });
            }

            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }

            // Chỉ cập nhật các thuộc tính được truyền từ client
            foreach (var property in typeof(BorrowerInformation).GetProperties())
            {
                var newValue = property.GetValue(borrowerInformation);
                if (newValue != null)
                {
                    property.SetValue(currentData, newValue);
                }
            }

            // Cập nhật bản ghi với các thuộc tính đã thay đổi
            Update(currentData);

            return new JsonResult(new { message = Constants.Message.UpdatedSuccessfully });
        }



        public IActionResult UploadImageBorrowerService(IFormFile file)
        {
            return UploadImage(file);
        }

    }
}