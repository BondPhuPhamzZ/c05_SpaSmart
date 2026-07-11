using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.PhieuTinhTien
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.PhieuTinhTien> PhieuTinhTiens { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PhieuTinhTiens != null)
            {
                PhieuTinhTiens = await _context.PhieuTinhTiens.OrderByDescending(p => p.NgayThanhToan).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync(string TenKhachHang, string SoDienThoai, string TenKtv, string HinhThucThanhToan, decimal TongTien)
        {
            var ptt = new c05_SpaSmart.Models.PhieuTinhTien
            {
                TenKhachHang = TenKhachHang,
                SoDienThoai = SoDienThoai,
                TenKtv = TenKtv,
                HinhThucThanhToan = HinhThucThanhToan,
                TongTien = TongTien,
                NgayThanhToan = System.DateTime.Now,
                TrangThai = "Hoàn tất"
            };
            
            _context.PhieuTinhTiens.Add(ptt);
            await _context.SaveChangesAsync();
            
            TempData["SuccessMessage"] = "Thanh toán thành công!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var ptt = await _context.PhieuTinhTiens.FindAsync(id);
            if (ptt != null)
            {
                _context.PhieuTinhTiens.Remove(ptt);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xóa hóa đơn!";
            }
            return RedirectToPage();
        }
    }
}
