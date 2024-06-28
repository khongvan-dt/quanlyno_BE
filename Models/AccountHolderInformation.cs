using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static quanLyNo.Common.Enums;

namespace quanLyNo.Models
{
    // Thông tin Chủ tài khoản
    public class AccountHolderInformation
    {
        [Key]
        public int Id { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual IdentityUser UserIdF { get; set; } // Thông tin người dùng mượn, đại diện cho khóa ngoại
        public string? FullName { get; set; } // Họ và tên đầy đủ của chủ tài khoản
        public string? PhoneNumber { get; set; } // Số điện thoại của chủ tài khoản
        public string? IdentityCardNumber { get; set; } // Số CCCD hoặc CMND của chủ tài khoản
        public DateTime? DateOfIssue { get; set; } // Ngày cấp CCCD hoặc CMND
        public string? PlaceOfIssue { get; set; } // Nơi cấp CCCD hoặc CMND
        public Gender? Gender { get; set; } // Giới tính của chủ tài khoản
        public DateTime? DateOfBirth { get; set; } // Ngày tháng năm sinh của chủ tài khoản
        public string? Hometown { get; set; } // Quê quán của chủ tài khoản
        public string? Address { get; set; } // Địa chỉ thường trú của chủ tài khoản
        public string? ImageBack { get; set; } // Ảnh mặt sau của CCCD hoặc CMND
        public string? ImageFront { get; set; } // Ảnh mặt trước của CCCD hoặc CMND
    }
}
