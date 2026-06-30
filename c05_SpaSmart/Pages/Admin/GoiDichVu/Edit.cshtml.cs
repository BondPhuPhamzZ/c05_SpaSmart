using System;
using System.Linq;
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
    public class EditModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public EditModel(SpaSmartDbContext context)
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

            var goidichvu =  await _context.GoiDichVus.FirstOrDefaultAsync(m => m.Id == id);
            if (goidichvu == null)
            {
                return NotFound();
            }
            GoiDichVu = goidichvu;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(GoiDichVu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoiDichVuExists(GoiDichVu.Id))
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

        private bool GoiDichVuExists(int id)
        {
          return (_context.GoiDichVus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
