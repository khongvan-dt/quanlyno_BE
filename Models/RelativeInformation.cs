using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    public class RelativeInformation
    {
        //thông tin người thân
        [Key]
        public int Id { get; set; } // Mã thông tin người thân
        public string? UserId { get; set; } // Mã người dùng (liên kết với bảng User)
        public int? BorrowerId { get; set; } // Mã người thân người vay (liên kết với bảng BorrowerInformation)
        [Required]
        public string FullName { get; set; } // Họ và tên đầy đủ của người thân người vay
        [Required]
        public string PhoneNumber { get; set; } // Số điện thoại của người thân người vay
        public string? Email { get; set; } // Email của người thân người vay
        [Required]
        public string IdentityCardNumber { get; set; } // Số CCCD hoặc CMND của người thân người vay
        public DateTime DateOfIssue { get; set; } // Ngày cấp CCCD hoặc CMND
        public string? PlaceOfIssue { get; set; } // Nơi cấp CCCD hoặc CMND
        [Required]
        public int Gender { get; set; } // Giới tính của người thân người vay
        public DateTime DateOfBirth { get; set; } // Ngày tháng năm sinh của người thân người vay
        [Required]
        public string Hometown { get; set; } // Quê quán của người thân người vay
        [Required]
        public string Address { get; set; } // Địa chỉ thường trú của người thân người vay
        [Required]
        public string ImageBack { get; set; } // Ảnh mặt sau của CCCD hoặc CMND của người thân người vay
        [Required]
        public string ImageFront { get; set; } // Ảnh mặt trước của CCCD hoặc CMND của người thân người vay
        [Required]
        public string Portrait { get; set; } // Ảnh chân dung
        public string? Note { get; set; } // Note
       [ForeignKey("UserId")]
        public virtual IdentityUser? UseuserIdForeignKeyrIdF { get; set; }
        [ForeignKey("BorrowerId")]
        public virtual BorrowerInformation? BorrowerIdF { get; set; }
    }
}
