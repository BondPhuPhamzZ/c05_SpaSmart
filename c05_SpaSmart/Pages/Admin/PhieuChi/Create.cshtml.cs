using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Data;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.PhieuChi
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public CreateModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PhieuChi = new c05_SpaSmart.Models.PhieuChi
            {
                NgayLap = DateTime.Now
            };
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

            _context.PhieuChis.Add(PhieuChi);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
