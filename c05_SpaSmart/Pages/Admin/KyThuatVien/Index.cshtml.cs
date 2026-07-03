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

namespace c05_SpaSmart.Pages.Admin.KyThuatVien
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public IndexModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.KyThuatVien> KyThuatVienList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.KyThuatViens != null)
            {
                KyThuatVienList = await _context.KyThuatViens.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int id, string newStatus)
        {
            var ktv = await _context.KyThuatViens.FindAsync(id);
            if (ktv == null) return NotFound();

            if (Enum.TryParse<TrangThaiKTV>(newStatus, out var parsedStatus))
            {
                // Kiểm tra logic theo Rule BR05: Không thể xếp nghỉ phép nếu KTV đang có lịch hẹn chờ hoàn thành
                if (parsedStatus == TrangThaiKTV.NghiPhep)
                {
                    var hasPendingBooking = await _context.LichHens.AnyAsync(l => l.KyThuatVienId == id && 
                        (l.TrangThai == TrangThaiLichHen.DaXacNhan || l.TrangThai == TrangThaiLichHen.DangPhucVu));
                    
                    if (hasPendingBooking)
                    {
                        TempData["ErrorMessage"] = "Lỗi: KTV đã có lịch hẹn với khách. Vui lòng dời lịch trước khi cho KTV nghỉ (MS12).";
                        return RedirectToPage("./Index");
                    }
                }

                ktv.TrangThaiKTV = parsedStatus;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật lịch làm việc của KTV thành công (MS11).";
            }

            return RedirectToPage("./Index");
        }
    }
}
