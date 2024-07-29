using System.Linq;
using Microsoft.AspNetCore.Mvc;
using quanLyNo_BE.Common;
using quanLyNo_BE.Controllers;
using quanLyNo_BE.Models;

namespace quanLyNo_BE.Services
{
    public class LoanRepaymentService : Repository<LoanRepayment>
    {
        private readonly ApplicationDbContext dc;

        public LoanRepaymentService(ApplicationDbContext dc) : base(dc)
        {
            this.dc = dc;
        }
        public IActionResult CreateLoanRepaymentService(LoanRepayment loanRepaymentItem)
        {
            if (loanRepaymentItem == null)
            {
                return BadRequest("LoanRepayment item is null.");
            }

            // Lấy LoanInformationId từ loanRepaymentItem
            var loanInformation = dc.Set<LoanInformation>()
                .FirstOrDefault(l => l.Id == loanRepaymentItem.LoanInformationId);

            if (loanInformation == null)
            {
                return BadRequest("LoanInformation not found with the specified LoanInformationId.");
            }

            if (loanRepaymentItem.AmountPaid > loanInformation.LoanAmount)
            {
                return BadRequest(Constants.Message.AmountPaidGreater);
            }

            if (ModelState.IsValid)
            {
                // Gọi phương thức Create từ lớp Repository
                Create(loanRepaymentItem);
                return BadRequest(Constants.Message.CreatedSuccessfully);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }

}
