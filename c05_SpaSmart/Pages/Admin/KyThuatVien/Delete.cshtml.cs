using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.KyThuatVien
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public DeleteModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public c05_SpaSmart.Models.KyThuatVien KyThuatVien { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.KyThuatViens == null)
            {
                return NotFound();
            }

            var kythuatvien = await _context.KyThuatViens.FirstOrDefaultAsync(m => m.Id == id);

            if (kythuatvien == null)
            {
                return NotFound();
            }
            else 
            {
                KyThuatVien = kythuatvien;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.KyThuatViens == null)
            {
                return NotFound();
            }
            var kythuatvien = await _context.KyThuatViens.FindAsync(id);

            if (kythuatvien != null)
            {
                KyThuatVien = kythuatvien;
                _context.KyThuatViens.Remove(KyThuatVien);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
