using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.GoiDichVu
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public DeleteModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public c05_SpaSmart.Models.GoiDichVu GoiDichVu { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GoiDichVus == null)
            {
                return NotFound();
            }

            var goidichvu = await _context.GoiDichVus.FirstOrDefaultAsync(m => m.Id == id);

            if (goidichvu == null)
            {
                return NotFound();
            }
            else 
            {
                GoiDichVu = goidichvu;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.GoiDichVus == null)
            {
                return NotFound();
            }
            var goidichvu = await _context.GoiDichVus.FindAsync(id);

            if (goidichvu != null)
            {
                GoiDichVu = goidichvu;
                _context.GoiDichVus.Remove(GoiDichVu);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
