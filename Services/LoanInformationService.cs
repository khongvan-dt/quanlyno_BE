using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Models;
using System.Collections.Generic;
using quanLyNo_BE.Repository;

namespace quanLyNo_BE.Services
{
    public class LoanInformationService : Repository<LoanInformation>
    {
        private readonly ApplicationDbContext dc;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LoanInformationService(ApplicationDbContext dc, IHttpContextAccessor httpContextAccessor)
                   : base(dc, httpContextAccessor)
        {
            this.dc = dc;
            _httpContextAccessor = httpContextAccessor;

        }
        public IActionResult CreateLoanInformationService(LoanInformation loanInformation)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            typeof(LoanInformation).GetProperty("UserId")?.SetValue(loanInformation, userId);
            // Nhân các thuộc tính kiểu int với 1
            var properties = typeof(LoanInformation).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    var currentValue = (int)property.GetValue(loanInformation);
                    property.SetValue(loanInformation, currentValue * 1);
                }
            }
            Create(loanInformation);
            return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });

        }
        public IEnumerable<LoanInformation> GetLoanÌnormationService()
        {
            var userId = GetUserIdFromToken();
            if (string.IsNullOrEmpty(userId))
            {
                new JsonResult(new { message = Constants.Message.UserIdEmpty });

            }
            var loanInformationList = base.Index();
            var dataList = loanInformationList
                .Where(x => IsUserIdMatch(x, userId)).ToList();
            return dataList;
        }
    }
}