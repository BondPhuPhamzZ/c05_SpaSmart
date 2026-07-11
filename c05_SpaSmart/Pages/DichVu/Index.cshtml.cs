using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Mvc;

namespace c05_SpaSmart.Pages.DichVu
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.DichVu> DichVus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DichVus != null)
            {
                DichVus = await _context.DichVus.Include(d => d.DanhMuc).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var dv = await _context.DichVus.FindAsync(id);
            if (dv != null)
            {
                _context.DichVus.Remove(dv);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
