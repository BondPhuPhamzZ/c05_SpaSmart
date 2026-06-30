using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;
using c05_SpaSmart.Models.ViewModels;

namespace c05_SpaSmart.Controllers
{
    [Authorize(Roles = "Customer")]
    public class BookingController : Controller
    {
        private readonly SpaSmartDbContext _context;

        public BookingController(SpaSmartDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Xem lịch sử đặt lịch
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var history = await _context.LichHens
                .Include(l => l.KyThuatVien)
                .Include(l => l.ChiTietLichHens!)
                    .ThenInclude(c => c.GoiDichVu)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.NgayGioDat)
                .ToListAsync();

            return View(history);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Services = await _context.GoiDichVus.ToListAsync();
            
            // Lấy danh sách KTV (Tất cả, hoặc chỉ những người sẵn sàng. Ở đây load tất cả để khách chọn yêu thích)
            ViewBag.KTVs = await _context.KyThuatViens.ToListAsync();

            var model = new BookingViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookingViewModel model)
        {
            if (model.NgayGioDat <= DateTime.Now)
            {
                ModelState.AddModelError("NgayGioDat", "Thời gian đặt lịch phải lớn hơn hiện tại.");
            }

            if (model.DichVuIds == null || !model.DichVuIds.Any())
            {
                ModelState.AddModelError("DichVuIds", "Bạn phải chọn ít nhất một dịch vụ.");
            }

            if (ModelState.IsValid)
            {
                // Quy định QĐ01: Kiểm tra trùng lịch KTV
                if (model.KyThuatVienId.HasValue)
                {
                    // Lấy các lịch hẹn Đã xác nhận hoặc Đang phục vụ của KTV này
                    var conflictingAppointments = await _context.LichHens
                        .Where(l => l.KyThuatVienId == model.KyThuatVienId.Value
                                 && (l.TrangThai == TrangThaiLichHen.DaXacNhan || l.TrangThai == TrangThaiLichHen.DangPhucVu))
                        .ToListAsync();

                    foreach(var app in conflictingAppointments)
                    {
                        // Kiểm tra khoảng thời gian (+- 90 phút mặc định)
                        if (Math.Abs((app.NgayGioDat - model.NgayGioDat).TotalMinutes) < 90)
                        {
                            ModelState.AddModelError("KyThuatVienId", "KTV bạn chọn đã kín lịch vào giờ này. Vui lòng chọn KTV khác hoặc đổi giờ.");
                            ViewBag.Services = await _context.GoiDichVus.ToListAsync();
                            ViewBag.KTVs = await _context.KyThuatViens.ToListAsync();
                            return View(model);
                        }
                    }
                }
                else
                {
                    // Hệ thống sẽ tự random KTV rảnh khi Admin duyệt (trong backend admin đã có chức năng này)
                }

                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                
                var lichHen = new LichHen
                {
                    NgayGioDat = model.NgayGioDat,
                    TrangThai = TrangThaiLichHen.ChoXacNhan,
                    GhiChu = model.GhiChu,
                    UserId = userId,
                    KyThuatVienId = model.KyThuatVienId,
                    TongTien = 0 // Sẽ tính sau
                };

                _context.LichHens.Add(lichHen);
                await _context.SaveChangesAsync(); // Lưu để có Id

                decimal total = 0;

                foreach (var svcId in model.DichVuIds!)
                {
                    var svc = await _context.GoiDichVus.FindAsync(svcId);
                    if (svc != null)
                    {
                        var chiTiet = new ChiTietLichHen
                        {
                            LichHenId = lichHen.Id,
                            DichVuId = svc.Id,
                            GiaTienLucDat = svc.GiaTien
                        };
                        _context.ChiTietLichHens.Add(chiTiet);
                        total += svc.GiaTien;
                    }
                }

                lichHen.TongTien = total;
                lichHen.TongThanhToan = total; // Tạm tính bằng tổng tiền, ưu đãi tính sau
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đặt lịch thành công! Lễ tân sẽ sớm liên hệ xác nhận.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Services = await _context.GoiDichVus.ToListAsync();
            ViewBag.KTVs = await _context.KyThuatViens.ToListAsync();
            return View(model);
        }
    }
}
