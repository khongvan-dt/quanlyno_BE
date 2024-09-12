using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? RToken { get; set; }
        public string? UserId { get; set; } // Mã người dùng (liên kết với bảng User)
        public DateTime CreatedDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public virtual IdentityUser? UserIdF { get; set; }

    }
}