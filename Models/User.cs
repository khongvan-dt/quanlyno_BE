using System.ComponentModel.DataAnnotations;

namespace quanLyNo.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; } // Mã người dùng
        public string UserName { get; set; } // Tên đăng nhập của người dùng
        public string Email { get; set; } // Địa chỉ email của người dùng
        public string Password { get; set; } // Mật khẩu của người dùng
        public int idRole { get; set; } // Vai trò của người dùng admin=0;  user=1
    }
}