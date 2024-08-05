using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Models;
using System.Collections.Generic;
using System;
using quanLyNo_BE.Repository;
namespace quanLyNo_BE.Services
{
    public class BorrowerInformationService : Repository<BorrowerInformation>
    {
        private readonly ApplicationDbContext dc;

        public BorrowerInformationService(ApplicationDbContext dc, IHttpContextAccessor httpContextAccessor)
            : base(dc, httpContextAccessor)
        {
            this.dc = dc;
        }
        public IActionResult CreateBorrowerInformationService(BorrowerInformation borrowerInformation)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            typeof(LoanRepayment).GetProperty("UserId")?.SetValue(borrowerInformation, userId);
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
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            typeof(BorrowerInformation).GetProperty("id")?.SetValue(borrowerInformation, id);
            Update(borrowerInformation);
            return new JsonResult(new { message = Constants.Message.UpdatedSuccessfully });
        }
        public IActionResult UploadImageBorrowerService(IFormFile file)
        {
            return UploadImage(file);
        }

    }
}