using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Models;
using System;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.PhieuChi
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
        public c05_SpaSmart.Models.PhieuChi PhieuChi { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PhieuChis == null || PhieuChi == null)
            {
                return Page();
            }

            PhieuChi.NgayChi = DateTime.Now;
            _context.PhieuChis.Add(PhieuChi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
