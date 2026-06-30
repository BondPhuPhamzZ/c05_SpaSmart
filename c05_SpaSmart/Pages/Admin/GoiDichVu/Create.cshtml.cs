using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.GoiDichVu
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
        public c05_SpaSmart.Models.GoiDichVu GoiDichVu { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.GoiDichVus == null || GoiDichVu == null)
            {
                return Page();
            }

            _context.GoiDichVus.Add(GoiDichVu);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
