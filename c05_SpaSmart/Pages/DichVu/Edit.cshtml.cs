using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Pages.DichVu
{
    public class EditModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public EditModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.DichVu DichVu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DichVus == null)
            {
                return NotFound();
            }

            var dichvu =  await _context.DichVus.FirstOrDefaultAsync(m => m.Id == id);
            if (dichvu == null)
            {
                return NotFound();
            }
            DichVu = dichvu;
            ViewData["DanhMucId"] = new SelectList(_context.DanhMucDichVus, "Id", "TenDanhMuc");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["DanhMucId"] = new SelectList(_context.DanhMucDichVus, "Id", "TenDanhMuc");
                return Page();
            }

            _context.Attach(DichVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DichVuExists(DichVu.Id))
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

        private bool DichVuExists(int id)
        {
          return (_context.DichVus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
