using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Models;
using quanLyNo_BE.Repository;
using quanLyNo_BE.Common;
using System.Linq;

namespace quanLyNo_BE.Services
{
    public class LoanContractService : Repository<LoanContract>
    {

        private readonly ApplicationDbContext dc;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoanContractService(ApplicationDbContext dbContext, IHttpContextAccessor _httpContextAccessor)
        : base(dbContext, _httpContextAccessor)
        {
            this.dc = dbContext;
            this._httpContextAccessor = _httpContextAccessor;
        }
        public IActionResult CreateLoanContractService(LoanContract loanContract)
        {
            var userId = GetUserIdFromToken();
            if (userId == null)
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            typeof(LoanContract).GetProperty("UserId")?.SetValue(loanContract, userId);
            // Nhân các thuộc tính kiểu int với 1
            var properties = typeof(LoanContract).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(int))
                {
                    var currentValue = (int)property.GetValue(loanContract);
                    property.SetValue(loanContract, currentValue * 1);
                }
            }
            Create(loanContract);
            return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });

        }
        public IEnumerable<LoanContract> GetBorrowerInformationService()
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

    }
}
