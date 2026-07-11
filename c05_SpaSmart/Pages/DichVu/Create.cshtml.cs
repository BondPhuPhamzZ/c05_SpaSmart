using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Pages.DichVu
{
    public class CreateModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public CreateModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["DanhMucId"] = new SelectList(_context.DanhMucDichVus, "Id", "TenDanhMuc");
            return Page();
        }

        [BindProperty]
        public c05_SpaSmart.Models.DichVu DichVu { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DichVus == null || DichVu == null)
            {
                ViewData["DanhMucId"] = new SelectList(_context.DanhMucDichVus, "Id", "TenDanhMuc");
                return Page();
            }

            _context.DichVus.Add(DichVu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
