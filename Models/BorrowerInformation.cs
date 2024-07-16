using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    //thông tin người vay
    public class BorrowerInformation
    {
        [Key]
        public int Id { get; set; } // Mã thông tin người vay
        public string? UserId { get; set; } // Mã người dùng (liên kết với bảng User)
        [Required]
        public string FullName { get; set; } // Họ và tên đầy đủ của người vay
        [Required]
        public string PhoneNumber { get; set; } // Số điện thoại của người vay
        public string? Email { get; set; } // Email của người vay
        [Required]
        public string IdentityCardNumber { get; set; } // Số CCCD hoặc CMND của người vay
        public DateTime? DateOfIssue { get; set; } // Ngày cấp CCCD hoặc CMND
        public string? PlaceOfIssue { get; set; } // Nơi cấp CCCD hoặc CMND
        [Required]
        public int Gender { get; set; } // Giới tính của người vay
        public DateTime DateOfBirth { get; set; } // Ngày tháng năm sinh của người vay
        [Required]
        public string Hometown { get; set; } // Quê quán của người vay
        [Required]
        public string Address { get; set; } // Địa chỉ thường trú của người vay
        [Required]
        public string ImageBack { get; set; } // Ảnh mặt sau của CCCD hoặc CMND của người vay
        [Required]
        public string ImageFront { get; set; } // Ảnh mặt trước của CCCD hoặc CMND của người vay
        [Required]
        public string Portrait { get; set; } // Ảnh chân dung
        [Required]
        public int LoanDone { get; set; } // Chả hết nợ hay chưa
        public string? Note { get; set; } // Note

        [ForeignKey("UserId")]
        public virtual IdentityUser? UserIdF { get; set; }
    }
}
