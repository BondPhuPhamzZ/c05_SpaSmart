using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Data
{
    public static class SpaSmartDbSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SpaSmartDbContext>();

                if (context != null)
                {
                    context.Database.EnsureCreated();

                    // Seed Users
                    if (!context.Users.Any())
                    {
                        context.Users.AddRange(new List<User>()
                        {
                            new User
                            {
                                Username = "admin",
                                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                                FullName = "Quản trị viên",
                                PhoneNumber = "0123456789",
                                Role = UserRole.Admin,
                                DiemTichLuy = 0
                            },
                            new User
                            {
                                Username = "khachhang",
                                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                                FullName = "Nguyễn Văn Khách",
                                PhoneNumber = "0987654321",
                                Role = UserRole.Customer,
                                DiemTichLuy = 100
                            }
                        });
                        context.SaveChanges();
                    }

                    // Seed Dịch vụ
                    if (!context.GoiDichVus.Any())
                    {
                        context.GoiDichVus.Add(new GoiDichVu
                        {
                            TenDichVu = "Massage Body 60 phút",
                            MoTa = "Thư giãn toàn thân",
                            GiaTien = 500000,
                            ThoiLuongPhut = 60
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
