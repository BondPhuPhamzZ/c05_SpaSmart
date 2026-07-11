using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Pages.DanhMucDichVu
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
            return Page();
        }

        [BindProperty]
        public c05_SpaSmart.Models.DanhMucDichVu DanhMuc { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.DanhMucDichVus == null || DanhMuc == null)
            {
                return Page();
            }

            _context.DanhMucDichVus.Add(DanhMuc);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
