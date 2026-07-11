using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Models;
using System;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.PhieuTinhTien
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
        public c05_SpaSmart.Models.PhieuTinhTien PhieuTinhTien { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PhieuTinhTiens == null || PhieuTinhTien == null)
            {
                return Page();
            }

            PhieuTinhTien.NgayThanhToan = DateTime.Now;
            PhieuTinhTien.TrangThai = "Hoàn tất";
            _context.PhieuTinhTiens.Add(PhieuTinhTien);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}