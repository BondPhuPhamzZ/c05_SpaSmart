using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.DichVu
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.DichVu> DichVus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DichVus != null)
            {
                DichVus = await _context.DichVus
                    .Include(d => d.DanhMuc).ToListAsync();
            }
            ViewData["DanhMucId"] = new SelectList(_context.DanhMucDichVus, "Id", "TenDanhMuc");
        }

        public async Task<IActionResult> OnPostCreateAsync(string TenDichVu, int Loai, int DanhMucId, decimal GiaTien, decimal GiamGia, string MoTa)
        {
            var dv = new c05_SpaSmart.Models.DichVu
            {
                TenDichVu = TenDichVu,
                Loai = Loai,
                DanhMucId = DanhMucId,
                GiaTien = GiaTien,
                GiamGia = GiamGia,
                MoTa = MoTa
            };
            _context.DichVus.Add(dv);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm thành công!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int Id, string TenDichVu, int Loai, int DanhMucId, decimal GiaTien, decimal GiamGia, string MoTa)
        {
            var dv = await _context.DichVus.FindAsync(Id);
            if (dv != null)
            {
                dv.TenDichVu = TenDichVu;
                dv.Loai = Loai;
                dv.DanhMucId = DanhMucId;
                dv.GiaTien = GiaTien;
                dv.GiamGia = GiamGia;
                dv.MoTa = MoTa;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật thành công!";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var dv = await _context.DichVus.FindAsync(id);
            if (dv != null)
            {
                _context.DichVus.Remove(dv);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xoá!";
            }
            return RedirectToPage();
        }
    }
}
