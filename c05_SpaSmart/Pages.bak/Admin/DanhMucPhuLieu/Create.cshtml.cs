using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.DanhMucPhuLieu
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public CreateModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DichVuId"] = new SelectList(_context.GoiDichVus, "Id", "TenDichVu");
            ViewData["PhuLieuId"] = new SelectList(_context.PhuLieus, "Id", "TenPhuLieu");
            return Page();
        }

        [BindProperty]
        public c05_SpaSmart.Models.DanhMucPhuLieu DanhMucPhuLieu { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DanhMucPhuLieus == null || DanhMucPhuLieu == null)
            {
                ViewData["DichVuId"] = new SelectList(_context.GoiDichVus, "Id", "TenDichVu");
                ViewData["PhuLieuId"] = new SelectList(_context.PhuLieus, "Id", "TenPhuLieu");
                return Page();
            }

            // Kiểm tra xem đã map chưa, nếu map rồi thì update
            var existing = await _context.DanhMucPhuLieus.FirstOrDefaultAsync(d => d.DichVuId == DanhMucPhuLieu.DichVuId && d.PhuLieuId == DanhMucPhuLieu.PhuLieuId);
            if (existing != null)
            {
                existing.SoLuongTieuHao = DanhMucPhuLieu.SoLuongTieuHao;
                _context.Attach(existing).State = EntityState.Modified;
            }
            else
            {
                _context.DanhMucPhuLieus.Add(DanhMucPhuLieu);
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Gắn định mức phụ liệu thành công! (MS09)";
            return RedirectToPage("/Admin/PhuLieu/Index");
        }
    }
}
