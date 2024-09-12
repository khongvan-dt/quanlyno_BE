using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace quanLyNo_BE.Models
{
    public class RefreshTokenRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Token { get; set; }
        public string RefreshToken { get; set; }
    }
}