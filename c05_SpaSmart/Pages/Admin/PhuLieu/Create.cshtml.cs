using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Data;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.PhuLieu
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
            return Page();
        }

        [BindProperty]
        public c05_SpaSmart.Models.PhuLieu PhuLieu { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PhuLieus == null || PhuLieu == null)
            {
                return Page();
            }

            _context.PhuLieus.Add(PhuLieu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
