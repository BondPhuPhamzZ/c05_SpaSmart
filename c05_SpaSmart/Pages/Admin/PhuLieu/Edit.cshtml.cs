using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.PhuLieu
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public EditModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public c05_SpaSmart.Models.PhuLieu PhuLieu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PhuLieus == null)
            {
                return NotFound();
            }

            var phulieu =  await _context.PhuLieus.FirstOrDefaultAsync(m => m.Id == id);
            if (phulieu == null)
            {
                return NotFound();
            }
            PhuLieu = phulieu;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (PhuLieu.DonGia < 0)
            {
                ModelState.AddModelError("PhuLieu.DonGia", "Giá phụ liệu không được là số âm. Vui lòng kiểm tra lại"); // MS10
                return Page();
            }

            // Alternative Process: Cập nhật tồn kho (UC03)
            // Thay vì chỉ Add mới, người dùng có thể Edit và cộng dồn số lượng.
            // Nhưng Edit này đang là ghi đè. Để làm đúng, ta cứ cho sửa số lượng tồn kho.
            _context.Attach(PhuLieu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật kho thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhuLieuExists(PhuLieu.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PhuLieuExists(int id)
        {
          return (_context.PhuLieus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
