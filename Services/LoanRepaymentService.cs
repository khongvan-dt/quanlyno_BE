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
    public class LoanRepaymentService : Repository<LoanRepayment>
    {
        private readonly ApplicationDbContext dc;

        public LoanRepaymentService(ApplicationDbContext dc, IHttpContextAccessor httpContextAccessor)
            : base(dc, httpContextAccessor)
        {
            this.dc = dc;
        }

        public string ValidateLoanRepayment(LoanRepayment loanRepaymentItem)
        {
            if (loanRepaymentItem == null)
            {
                return "LoanRepayment item is null.";
            }

            var loanInformation = dc.Set<LoanInformation>()
                .FirstOrDefault(l => l.Id == loanRepaymentItem.LoanInformationId);

            if (loanInformation == null)
            {
                return "LoanInformation not found with the specified LoanInformationId.";
            }

            if (loanRepaymentItem.AmountPaid > loanInformation.LoanAmount)
            {
                return Constants.Message.AmountPaidGreater;
            }

            return null;
        }

        public IActionResult CreateLoanRepaymentService(LoanRepayment loanRepaymentItem)
        {
            var userId = GetUserIdFromToken();
            Console.WriteLine("UserId insert: " + userId);

            if (string.IsNullOrEmpty(userId))
            {
                return new JsonResult(new { message = Constants.Message.UserIdEmpty });
            }
            var validationResult = ValidateLoanRepayment(loanRepaymentItem);
            if (validationResult != null)
            {
                return new JsonResult(new { message = validationResult });
            }

            typeof(LoanRepayment).GetProperty("UserId")?.SetValue(loanRepaymentItem, userId);

            Create(loanRepaymentItem);
            return new JsonResult(new { message = Constants.Message.CreatedSuccessfully });
        }

        public IEnumerable<LoanRepayment> GetLoanRepayment()
        {
            var userId = GetUserIdFromToken();

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException(Constants.Message.InvalidToken);
            }
            var loanRepayments = base.Index();
            var filteredLoanRepayments = loanRepayments.Where(e => IsUserIdMatch(e, userId)).ToList();
            return filteredLoanRepayments;
        }
    }

}


