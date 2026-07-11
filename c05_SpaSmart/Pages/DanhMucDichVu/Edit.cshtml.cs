using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Pages.DanhMucDichVu
{
    public class EditModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public EditModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.DanhMucDichVu DanhMuc { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.DanhMucDichVus == null)
            {
                return NotFound();
            }

            var danhmuc =  await _context.DanhMucDichVus.FirstOrDefaultAsync(m => m.Id == id);
            if (danhmuc == null)
            {
                return NotFound();
            }
            DanhMuc = danhmuc;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(DanhMuc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DanhMucExists(DanhMuc.Id))
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

        private bool DanhMucExists(int id)
        {
          return (_context.DanhMucDichVus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
