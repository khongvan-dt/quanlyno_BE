using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    public class LoanDone
    {
        [Key]
        public int Id { get; set; } // Mã thông tin trả nợ
        public string UserId { get; set; } // Mã người dùng (liên kết với bảng User)

        [ForeignKey("UserId")]
        public virtual IdentityUser? userIdForeignKey { get; set; }
        public int LoanInformationId { get; set; } // Mã thông tin khoản vay (liên kết với bảng LoanInformation)
        public int BorrowerInformationId { get; set; } // Liên kết với người vay
        public decimal AmountPaid { get; set; } // Số tiền đã trả
        public DateTime PaymentDate { get; set; } // Ngày trả
        public int? IsInstallment { get; set; } // Kiểu vay: true nếu vay trả góp, false nếu vay trả một cục

        public string? Note { get; set; } // Note       

        [ForeignKey("LoanInformationId")]
        public virtual LoanInformation? LoanInformationidF { get; set; } // Tham chiếu đến bảng thông tin khoản vay
        [ForeignKey("BorrowerInformationId")]
        public virtual BorrowerInformation? BorrowerInformationIdF { get; set; }
    }
}
