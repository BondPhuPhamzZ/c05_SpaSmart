using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.LichHen
{
    [Authorize(Roles = "Admin")]
    public class ThanhToanModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public ThanhToanModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public c05_SpaSmart.Models.LichHen LichHen { get; set; } = default!;
        public decimal TongTienDichVu { get; set; }
        public decimal TienGiamGia { get; set; }
        public decimal TongThanhToan { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var lichHen = await _context.LichHens
                .Include(l => l.User)
                .Include(l => l.ChiTietLichHens)
                    .ThenInclude(c => c.GoiDichVu)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (lichHen == null || lichHen.TrangThai == TrangThaiLichHen.DaThanhToan)
            {
                return NotFound();
            }

            LichHen = lichHen;
            
            // Tính toán
            TongTienDichVu = lichHen.ChiTietLichHens.Sum(c => c.GiaTienLucDat);
            TienGiamGia = 0;
            
            // Áp dụng giảm giá 10% nếu > 2.000.000
            if (TongTienDichVu > 2000000)
            {
                TienGiamGia = TongTienDichVu * 0.1m;
            }

            TongThanhToan = TongTienDichVu - TienGiamGia;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, decimal tongTien, decimal tienGiam, decimal thanhToan)
        {
            var lichHen = await _context.LichHens.FindAsync(id);
            if (lichHen == null) return NotFound();

            // Lưu hóa đơn
            lichHen.TrangThai = TrangThaiLichHen.DaThanhToan;
            lichHen.NgayLapHoaDon = DateTime.Now;
            lichHen.TongTien = tongTien;
            lichHen.TienGiamGia = tienGiam;
            lichHen.TongThanhToan = thanhToan;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
