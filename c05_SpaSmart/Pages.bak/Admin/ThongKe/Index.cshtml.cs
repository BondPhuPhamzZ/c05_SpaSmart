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

namespace c05_SpaSmart.Pages.Admin.ThongKe
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public IndexModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        public decimal TongThuThangNay { get; set; }
        public decimal TongChiThangNay { get; set; }
        public decimal LoiNhuanThangNay { get; set; }

        public decimal TongThuTatCa { get; set; }
        public decimal TongChiTatCa { get; set; }
        public decimal LoiNhuanTatCa { get; set; }

        public async Task OnGetAsync()
        {
            var today = DateTime.Today;
            var startOfMonth = new DateTime(today.Year, today.Month, 1);

            // Thống kê tháng này
            TongThuThangNay = await _context.LichHens
                .Where(l => l.TrangThai == TrangThaiLichHen.DaThanhToan && l.NgayLapHoaDon >= startOfMonth)
                .SumAsync(l => l.TongThanhToan);

            TongChiThangNay = await _context.PhieuChis
                .Where(p => p.NgayLap >= startOfMonth)
                .SumAsync(p => p.SoTien);

            LoiNhuanThangNay = TongThuThangNay - TongChiThangNay;

            // Thống kê toàn thời gian
            TongThuTatCa = await _context.LichHens
                .Where(l => l.TrangThai == TrangThaiLichHen.DaThanhToan)
                .SumAsync(l => l.TongThanhToan);

            TongChiTatCa = await _context.PhieuChis.SumAsync(p => p.SoTien);

            LoiNhuanTatCa = TongThuTatCa - TongChiTatCa;
        }
    }
}
