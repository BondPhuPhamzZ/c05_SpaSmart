using System;
using System.Collections.Generic;
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
    public class IndexModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public IndexModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.LichHen> LichHenList { get;set; } = default!;
        public IList<c05_SpaSmart.Models.KyThuatVien> KtvSanSangList { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.LichHens != null)
            {
                LichHenList = await _context.LichHens
                    .Include(l => l.KyThuatVien)
                    .Include(l => l.User)
                    .Include(l => l.ChiTietLichHens)
                        .ThenInclude(c => c.GoiDichVu)
                    .OrderByDescending(l => l.NgayGioDat)
                    .ToListAsync();
            }

            if (_context.KyThuatViens != null)
            {
                // Chỉ lấy các KTV đang rảnh rỗi để có thể gán lịch
                KtvSanSangList = await _context.KyThuatViens
                    .Where(k => k.TrangThaiKTV == TrangThaiKTV.SanSang)
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, int status, int? ktvId)
        {
            var lichHen = await _context.LichHens.Include(l => l.KyThuatVien).FirstOrDefaultAsync(l => l.Id == id);
            
            if (lichHen != null)
            {
                var newStatus = (TrangThaiLichHen)status;

                // Cập nhật trạng thái Lịch Hẹn
                lichHen.TrangThai = newStatus;

                // Nếu xác nhận và có chọn KTV
                if (newStatus == TrangThaiLichHen.DaXacNhan && ktvId.HasValue)
                {
                    lichHen.KyThuatVienId = ktvId.Value;
                }

                // Cập nhật trạng thái KTV cho logic thực tế
                if (newStatus == TrangThaiLichHen.DangPhucVu && lichHen.KyThuatVien != null)
                {
                    lichHen.KyThuatVien.TrangThaiKTV = TrangThaiKTV.DangBan;
                }
                else if (newStatus == TrangThaiLichHen.DaPhucVuXong && lichHen.KyThuatVien != null)
                {
                    lichHen.KyThuatVien.TrangThaiKTV = TrangThaiKTV.SanSang;
                }

                await _context.SaveChangesAsync();
            }
            return RedirectToPage("./Index");
        }
    }
}
