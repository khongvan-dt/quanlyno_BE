using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    // ghi số tiền đã trả,
    public class LoanRepayment
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; } // Mã người dùng (liên kết với bảng User)
        public int BorrowerInformationId { get; set; } // Liên kết với người vay
        public int LoanInformationId { get; set; } // Mã thông tin khoản vay (liên kết với bảng LoanInformation)
        public decimal AmountPaid { get; set; } // Số tiền đã trả
        public DateTime PaymentDate { get; set; } // Ngày trả nợ
        public string? Note { get; set; } // Note       
        [ForeignKey("UserId")]
         public virtual IdentityUser UserIdF { get; set; }

        [ForeignKey("BorrowerInformationId")]
        public virtual BorrowerInformation BorrowerInformationIdF { get; set; }
        [ForeignKey("LoanInformationId")]
        public virtual LoanInformation LoanInformationidF { get; set; } // Tham chiếu đến bảng thông tin khoản vay


    }
}
