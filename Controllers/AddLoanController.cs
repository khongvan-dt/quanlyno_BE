using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using quanLyNo.Common;
using quanLyNo.Models;

namespace quanLyNo.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api/[controller]")]
    public class LoanData
    {
        public BorrowerInformation borrowerInformation { get; set; }
        public RelativeInformation relativeInformation { get; set; }
        public LoanInformation loanInformation { get; set; }
        public LoanDone loanDone { get; set; }
    }

    public class AddLoanController : ControllerBase
    {
        ApplicationDbContext dc;

        public AddLoanController(ApplicationDbContext context)
        {
            dc = context;
        }

        [HttpPost]
        // public IActionResult Create([FromBody] loanData.borrowerInformation loanData.borrowerInformation,
        //  loanData.relativeInformation loanData.relativeInformation, loanData.loanInformation loanData.loanInformation, LoanDone loanDone)
        // {
        public IActionResult Create([FromBody] LoanData loanData)
        {
            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.FullName)
            )
            {
                return BadRequest(Constants.Message.FullName);
            }
            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.PhoneNumber)
            )
            {
                return BadRequest(Constants.Message.PhoneNumber);
            }
            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.Email)
            )
            {
                return BadRequest(Constants.Message.Email);
            }
            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.IdentityCardNumber)
            )
            {
                return BadRequest(Constants.Message.IdentityCardNumber);
            }
            if (
                loanData.borrowerInformation == null
                || loanData.borrowerInformation.DateOfIssue == null
            )
            {
                return BadRequest(Constants.Message.DateOfIssue);
            }

            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.PlaceOfIssue)
            )
            {
                return BadRequest(Constants.Message.PlaceOfIssue);
            }

            if (loanData.borrowerInformation == null || loanData.borrowerInformation.Gender > 0)
            {
                return BadRequest(Constants.Message.Gender);
            }
            if (
                loanData.borrowerInformation == null
                || loanData.borrowerInformation.DateOfBirth == null
            )
            {
                return BadRequest(Constants.Message.DateOfBirth);
            }
            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.Hometown)
            )
            {
                return BadRequest(Constants.Message.Hometown);
            }
            if (
                loanData.borrowerInformation == null
                || loanData.borrowerInformation.Address == null
            )
            {
                return BadRequest(Constants.Message.Address);
            }

            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.ImageBack)
            )
            {
                return BadRequest(Constants.Message.ImageBack);
            }

            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.Portrait)
            )
            {
                return BadRequest(Constants.Message.Portrait);
            }
            if (loanData.borrowerInformation == null || loanData.borrowerInformation.LoanDone <= 0)
            {
                return BadRequest(Constants.Message.LoanDone);
            }

            if (
                loanData.borrowerInformation == null
                || string.IsNullOrEmpty(loanData.borrowerInformation.Note)
            )
            {
                return BadRequest(Constants.Message.Note);
            }

            dc.BorrowerInformations.Add(loanData.borrowerInformation);
            dc.SaveChanges();

            if (
                loanData.relativeInformation == null
                || string.IsNullOrEmpty(loanData.relativeInformation.FullName)
            )
            {
                return BadRequest(Constants.Message.FullName);
            }
            if (
                loanData.relativeInformation == null
                || string.IsNullOrEmpty(loanData.relativeInformation.PhoneNumber)
            )
            {
                return BadRequest(Constants.Message.PhoneNumber);
            }
            dc.RelativeInformations.Add(loanData.relativeInformation);
            dc.SaveChanges();

            if (loanData == null)
            {
                return BadRequest(Constants.Message.loan);
            }

            if (
                loanData.loanInformation == null
                || string.IsNullOrEmpty(loanData.loanInformation.LoanInformationName)
            )
            {
                return BadRequest(Constants.Message.loanInformationName);
            }
            if (
                loanData.loanInformation == null
                || string.IsNullOrEmpty(loanData.loanInformation.Lender)
            )
            {
                return BadRequest(Constants.Message.Lender);
            }
            dc.LoanInformations.Add(loanData.loanInformation);
            dc.SaveChanges();

            if (loanData.loanDone == null || loanData.loanDone.AmountPaid == null)
            {
                return BadRequest(Constants.Message.AmountPaid);
            }
            if (loanData.loanDone == null || loanData.loanDone.PaymentDate == null)
            {
                return BadRequest(Constants.Message.PaymentDate);
            }
            dc.LoanDones.Add(loanData.loanDone);
            dc.SaveChanges();

            return Ok(Constants.Message.CreatedSuccessfully);
        }
    }
}
