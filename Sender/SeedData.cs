using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace quanLyNo_BE.Sender
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            // Tạo các vai trò (roles) nếu chưa tồn tại
            string[] roles = { "Admin", "User" }; // Danh sách vai trò cần tạo
            foreach (var role in roles)
            {
                // Kiểm tra xem vai trò đã tồn tại trong hệ thống chưa
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    // Nếu vai trò chưa tồn tại, tạo mới vai trò
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            // Tạo tài khoản admin nếu chưa tồn tại
            var adminUser = await userManager.FindByNameAsync("admin12");
            if (adminUser == null)
            {
                // Tạo tài khoản admin mới
                adminUser = new IdentityUser
                {
                    UserName = "admin12",
                    Email = "admin12@example.com"
                };
                var result = await userManager.CreateAsync(adminUser, "Admin12@123V"); // Tạo tài khoản với mật khẩu
                if (result.Succeeded)
                {
                    // Gán vai trò Admin và User cho tài khoản admin
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    await userManager.AddToRoleAsync(adminUser, "User");
                }
            }

            // Tạo tài khoản người dùng bình thường nếu chưa tồn tại
            var normalUser = await userManager.FindByNameAsync("user12");
            if (normalUser == null)
            {
                // Tạo tài khoản người dùng mới
                normalUser = new IdentityUser
                {
                    UserName = "user13",
                    Email = "user12@example.com"
                };
                var result = await userManager.CreateAsync(normalUser, "User12@123V"); // Tạo tài khoản với mật khẩu
                if (result.Succeeded)
                {
                    // Gán vai trò User cho tài khoản người dùng
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
        }

    }
}