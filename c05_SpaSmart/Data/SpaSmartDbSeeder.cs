using System;
using System.Linq;
using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace c05_SpaSmart.Data
{
    public static class SpaSmartDbSeeder
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<SpaSmartDbContext>();
                
                // Tự động tạo DB nếu chưa có
                context.Database.EnsureCreated();

                // Seed Users
                if (!context.Users.Any())
                {
                    context.Users.AddRange(new User[]
                    {
                        new User
                        {
                            Username = "admin",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                            FullName = "Quản trị viên",
                            PhoneNumber = "0900000000",
                            Role = UserRole.Admin,
                            DiemTichLuy = 0
                        },
                        new User
                        {
                            Username = "khachhang1",
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                            FullName = "Nguyễn Văn Khách",
                            PhoneNumber = "0911111111",
                            Role = UserRole.Customer,
                            DiemTichLuy = 150
                        }
                    });
                    context.SaveChanges();
                }

                // Seed Kỹ Thuật Viên
                if (!context.KyThuatViens.Any())
                {
                    context.KyThuatViens.AddRange(new KyThuatVien[]
                    {
                        new KyThuatVien
                        {
                            HoTen = "Trần Thị A",
                            ChuyenMon = "Massage Body, Chăm sóc da",
                            TrangThaiKTV = TrangThaiKTV.SanSang
                        },
                        new KyThuatVien
                        {
                            HoTen = "Lê Văn B",
                            ChuyenMon = "Bấm huyệt, Trị liệu vai gáy",
                            TrangThaiKTV = TrangThaiKTV.SanSang
                        }
                    });
                    context.SaveChanges();
                }

                // Seed Phụ liệu
                if (!context.PhuLieus.Any())
                {
                    context.PhuLieus.AddRange(new PhuLieu[]
                    {
                        new PhuLieu
                        {
                            TenPhuLieu = "Tinh dầu Massage Hoa Hồng 100ml",
                            HangSanXuat = "Cung Đình",
                            TonKho = 50,
                            DonGia = 150000
                        },
                        new PhuLieu
                        {
                            TenPhuLieu = "Mặt nạ bùn khoáng",
                            HangSanXuat = "Innisfree",
                            TonKho = 100,
                            DonGia = 25000
                        }
                    });
                    context.SaveChanges();
                }

                // Seed Gói dịch vụ
                if (!context.GoiDichVus.Any())
                {
                    context.GoiDichVus.AddRange(new GoiDichVu[]
                    {
                        new GoiDichVu
                        {
                            TenDichVu = "Massage Body Thư giãn",
                            MoTa = "Massage toàn thân 60 phút giúp lưu thông khí huyết",
                            GiaTien = 350000,
                            ThoiLuongPhut = 60
                        },
                        new GoiDichVu
                        {
                            TenDichVu = "Chăm sóc da mặt chuyên sâu",
                            MoTa = "Làm sạch sâu, đắp mặt nạ và massage da mặt",
                            GiaTien = 450000,
                            ThoiLuongPhut = 90
                        }
                    });
                    context.SaveChanges();
                }

                // Seed Danh Mục Phụ Liệu (Mapping)
                if (!context.DanhMucPhuLieus.Any())
                {
                    var massageDichVu = context.GoiDichVus.FirstOrDefault(d => d.TenDichVu.Contains("Massage Body"));
                    var chamSocDaDichVu = context.GoiDichVus.FirstOrDefault(d => d.TenDichVu.Contains("Chăm sóc da"));
                    var tinhDau = context.PhuLieus.FirstOrDefault(p => p.TenPhuLieu.Contains("Tinh dầu"));
                    var matNa = context.PhuLieus.FirstOrDefault(p => p.TenPhuLieu.Contains("Mặt nạ"));

                    if (massageDichVu != null && tinhDau != null)
                    {
                        context.DanhMucPhuLieus.Add(new DanhMucPhuLieu
                        {
                            DichVuId = massageDichVu.Id,
                            PhuLieuId = tinhDau.Id,
                            SoLuongTieuHao = 1 // 1 lọ
                        });
                    }

                    if (chamSocDaDichVu != null && matNa != null)
                    {
                        context.DanhMucPhuLieus.Add(new DanhMucPhuLieu
                        {
                            DichVuId = chamSocDaDichVu.Id,
                            PhuLieuId = matNa.Id,
                            SoLuongTieuHao = 1 // 1 gói
                        });
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
