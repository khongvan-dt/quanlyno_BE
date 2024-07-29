using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using quanLyNo_BE.Models;
using quanLyNo_BE.Services;

namespace quanLyNo_BE.Controllers
{
    [ApiController]
    [EnableCors()]
    [Route("api")]
    public class LoanRepaymentController : ControllerBase
    {

        private readonly LoanRepaymentService _loanRepaymentService;

        public LoanRepaymentController([FromBody] LoanRepaymentService loanRepaymentService)
        {
            _loanRepaymentService = loanRepaymentService;
        }
        [HttpPost("CreateLoanRepayment")]
        public IActionResult CreateLoanRepayment(LoanRepayment loanRepaymentItem)
        {
            Console.WriteLine(loanRepaymentItem);
            return _loanRepaymentService.CreateLoanRepaymentService(loanRepaymentItem);
        }
    }
}




// using quanLyNo_BE.Models;
// using Microsoft.AspNetCore.Cors;
// using Microsoft.AspNetCore.Mvc;
// using System.IdentityModel.Tokens.Jwt;
// using System.Linq;
// using System.Security.Claims;
// using System;
// using Microsoft.AspNetCore.Http;
// using quanLyNo_BE.Common;

// namespace quanLyNo_BE.Controllers
// {
//     [ApiController]
//     [EnableCors()]
//     [Route("api")]
//     public class LoanRepaymentController : Repository<LoanRepayment>
//     {
//         private readonly ApplicationDbContext dc;

//         public LoanRepaymentController(ApplicationDbContext dc) : base(dc)
//         {
//             this.dc = dc;
//         }

//         [HttpPost("CreateLoanRepayment")]
//         public IActionResult CreateLoanRepayment([FromBody] LoanRepayment loanRepaymentItem)
//         {
//             if (loanRepaymentItem == null)
//             {
//                 return BadRequest("LoanRepayment item is null.");
//             }

//             // Lấy LoanInformationId từ loanRepaymentItem
//             var loanInformation = dc.Set<LoanInformation>()
//                 .FirstOrDefault(l => l.Id == loanRepaymentItem.LoanInformationId);

//             if (loanInformation == null)
//             {
//                 return BadRequest("LoanInformation not found with the specified LoanInformationId.");
//             }

//             if (loanRepaymentItem.AmountPaid > loanInformation.LoanAmount)
//             {
//                 return BadRequest(Constants.Message.AmountPaidGreater);
//             }

//             if (ModelState.IsValid)
//             {
//                 // Gọi phương thức Create từ lớp Repository
//                 Create(loanRepaymentItem);
//                 return BadRequest(Constants.Message.CreatedSuccessfully);
//             }
//             else
//             {
//                 return BadRequest(ModelState);
//             }
//         }


//     }
// }
