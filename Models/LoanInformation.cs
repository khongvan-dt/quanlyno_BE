using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
  // Thông tin khoản vay
  public class LoanInformation
  {
    [Key]
    public int Id { get; set; } // Mã thông tin khoản vay
    public string? UserId { get; set; } // Mã người dùng (liên kết với bảng User)
    [Required]
    public string LoanInformationName { get; set; } // tên 
    public int? BorrowerId { get; set; } // Mã người vay (liên kết với bảng BorrowerInformation)
    public string? Lender { get; set; } // Công ty cho vay hoặc người cho vay
    public int? LoanType { get; set; } // Loại hình vay vốn
    public string? LoanPurpose { get; set; } // Mục đích vay vốn
    [Required]
    public float LoanAmount { get; set; } // Số tiền vay/
    [Required]
    public float InterestRate { get; set; } // Lãi suất/năm
    [Required]
    public int LoanTerm { get; set; } // Thời hạn vay
    [Required]
    public DateTime LoanDate { get; set; } // Ngày vay
    [Required]
    public DateTime RepaymentDate { get; set; } // Ngày trả
    [Required]
    public int IsInstallment { get; set; } // Kiểu vay: 1 nếu vay trả góp, 2 nếu vay trả một cục
    public float? MonthlyPayment { get; set; } // Số tiền gốc cần trả mỗi tháng
    public float Interest { get; set; } // Số tiền lãi cần trả mỗi tháng
    public string? Note { get; set; } // Note       
    [ForeignKey("UserId")]
    public virtual IdentityUser? UserIdF { get; set; }
    [ForeignKey("BorrowerId")]
    public virtual BorrowerInformation? BorrowerIdF { get; set; }
  }
}