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

namespace c05_SpaSmart.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly SpaSmartDbContext _context;

        public BookingController(SpaSmartDbContext context)
        {
            _context = context;
        }

        // GET: Booking
        // Hiển thị danh sách dịch vụ để khách chọn
        public async Task<IActionResult> Index()
        {
            var dichVus = await _context.GoiDichVus.ToListAsync();
            return View(dichVus);
        }

        // GET: Booking/Create/{dichVuId}
        public async Task<IActionResult> Create(int dichVuId)
        {
            var dichVu = await _context.GoiDichVus.FindAsync(dichVuId);
            if (dichVu == null) return NotFound();

            ViewBag.DichVu = dichVu;
            
            // Lấy danh sách KTV
            ViewBag.KTVs = await _context.KyThuatViens.ToListAsync();

            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int DichVuId, int? KyThuatVienId, DateTime NgayGioDat, string GhiChu)
        {
            var dichVu = await _context.GoiDichVus.FindAsync(DichVuId);
            if (dichVu == null) return NotFound();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            int userId = int.Parse(userIdString);

            // Validate thời gian
            if (NgayGioDat <= DateTime.Now)
            {
                ModelState.AddModelError("NgayGioDat", "Không được đặt lịch vào thời gian trong quá khứ.");
                ViewBag.DichVu = dichVu;
                ViewBag.KTVs = await _context.KyThuatViens.ToListAsync();
                return View();
            }

            // Tạo Lịch hẹn
            var lichHen = new LichHen
            {
                NgayGioDat = NgayGioDat,
                TrangThai = TrangThaiLichHen.ChoXacNhan,
                GhiChu = GhiChu,
                TongTien = dichVu.GiaTien, // Tạm tính bằng tiền dịch vụ
                UserId = userId,
                KyThuatVienId = KyThuatVienId
            };

            _context.LichHens.Add(lichHen);
            await _context.SaveChangesAsync();

            // Lưu chi tiết
            var chiTiet = new ChiTietLichHen
            {
                LichHenId = lichHen.Id,
                DichVuId = dichVu.Id,
                GiaTienLucDat = dichVu.GiaTien
            };
            
            _context.ChiTietLichHens.Add(chiTiet);
            await _context.SaveChangesAsync();

            return RedirectToAction("Success", new { id = lichHen.Id });
        }

        public IActionResult Success(int id)
        {
            ViewBag.LichHenId = id;
            return View();
        }

        // GET: Booking/History
        // Hiển thị lịch sử đặt lịch của khách hàng
        public async Task<IActionResult> History()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString)) return Unauthorized();

            int userId = int.Parse(userIdString);

            // Lấy danh sách lịch hẹn
            var lichHens = await _context.LichHens
                .Include(l => l.ChiTietLichHens)
                    .ThenInclude(c => c.GoiDichVu)
                .Include(l => l.KyThuatVien)
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.NgayGioDat)
                .ToListAsync();

            return View(lichHens);
        }
    }
}
