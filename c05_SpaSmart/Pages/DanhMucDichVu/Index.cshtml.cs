using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.DanhMucDichVu
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.DanhMucDichVu> DanhMucDichVus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DanhMucDichVus != null)
            {
                DanhMucDichVus = await _context.DanhMucDichVus.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync(string TenDanhMuc, string MoTa)
        {
            var dm = new c05_SpaSmart.Models.DanhMucDichVu
            {
                TenDanhMuc = TenDanhMuc,
                MoTa = MoTa
            };
            _context.DanhMucDichVus.Add(dm);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm danh mục thành công!";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync(int Id, string TenDanhMuc, string MoTa)
        {
            var dm = await _context.DanhMucDichVus.FindAsync(Id);
            if (dm != null)
            {
                dm.TenDanhMuc = TenDanhMuc;
                dm.MoTa = MoTa;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var dm = await _context.DanhMucDichVus.FindAsync(id);
            if (dm != null)
            {
                _context.DanhMucDichVus.Remove(dm);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Đã xoá danh mục!";
            }
            return RedirectToPage();
        }
    }
}
