using System;
using System.Linq;
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
    public class EditModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public EditModel(SpaSmartDbContext context)
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

            var kythuatvien =  await _context.KyThuatViens.FirstOrDefaultAsync(m => m.Id == id);
            if (kythuatvien == null)
            {
                return NotFound();
            }
            KyThuatVien = kythuatvien;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(KyThuatVien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KyThuatVienExists(KyThuatVien.Id))
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

        private bool KyThuatVienExists(int id)
        {
          return (_context.KyThuatViens?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
