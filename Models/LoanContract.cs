using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    public class LoanContract
    {
        // Hợp đồng vay
        [Key]
        public int Id { get; set; } // Mã hợp đồng vay
         public string? UserId { get; set; } // Mã người dùng (liên kết với bảng User)
        public int? LoanInformationId { get; set; } // Liên kết với thông tin khoản vay
        public int BorrowerInformationId { get; set; } // Liên kết với người vay
        public string? LoanRequestFormImage { get; set; } // Ảnh giấy đề nghị vay vốn
        public string? IncomeProofImage { get; set; } // Ảnh giấy tờ chứng minh thu nhập
        public string? LoanPurposeProofImage { get; set; } // Ảnh giấy tờ chứng minh mục đích vay vốn
        public string? CollateralImage { get; set; } // Ảnh tài sản đảm bảo
        public string? CompartmentId { get; set; } // Mã ngăn tầng cất hợp đồng
        public string? Note { get; set; } // Note    
           
        [ForeignKey("UserId")]
        // public virtual User? userIdF { get; set; }
        public virtual IdentityUser UserIdF { get; set; }

        [ForeignKey("LoanInformationId")]
        public virtual LoanInformation? LoanInformationF { get; set; } // Tham chiếu đến thông tin khoản vay

        [ForeignKey("BorrowerInformationId")]
        public virtual BorrowerInformation BorrowerInformationIdF { get; set; }
    }
}