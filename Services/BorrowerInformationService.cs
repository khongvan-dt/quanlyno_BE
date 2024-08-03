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
        public IActionResult CreateBorrowerInformation(BorrowerInformation borrowerInformation)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            typeof(LoanRepayment).GetProperty("UserId")?.SetValue(borrowerInformation, userId);
            Create(borrowerInformation);
            return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });

        }
        public IEnumerable<BorrowerInformation> GetBorrowerInformation()
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

        public IActionResult DeleteBorrowerInformation(int id)
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            // var borrowerInformation = GetById(id);
            // if (borrowerInformation == null)
            // {
            //     return new JsonResult(new { message = Constants.Message.NoDataFound });
            // }
            // if (!IsUserIdMatch(borrowerInformation, userId))
            // {
            //     return new JsonResult(new { message = Constants.Message.NoDataFound });
            // }

            Delete(id);
            return new JsonResult(new { message = Constants.Message.DeletedSuccessfully });
        }
        public IActionResult GetBorrowerId(int id)
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
    }
}