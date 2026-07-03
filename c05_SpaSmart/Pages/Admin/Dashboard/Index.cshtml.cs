using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using Microsoft.AspNetCore.Authorization;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Pages.Admin.Dashboard
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public IndexModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public int ChoXacNhanCount { get; set; }
        public int LichHenHomNayCount { get; set; }
        public decimal DoanhThuThangNay { get; set; }
        public int KTVRanhCount { get; set; }

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;
            var thisMonthStart = new DateTime(today.Year, today.Month, 1);

            ChoXacNhanCount = await _context.LichHens
                .Where(l => l.TrangThai == TrangThaiLichHen.ChoXacNhan)
                .CountAsync();

            LichHenHomNayCount = await _context.LichHens
                .Where(l => l.NgayGioDat.Date == today)
                .CountAsync();

            DoanhThuThangNay = await _context.LichHens
                .Where(l => l.TrangThai == TrangThaiLichHen.DaThanhToan && l.NgayLapHoaDon >= thisMonthStart)
                .SumAsync(l => l.TongThanhToan);

            KTVRanhCount = await _context.KyThuatViens
                .Where(k => k.TrangThaiKTV == TrangThaiKTV.SanSang)
                .CountAsync();
        }
    }
}
