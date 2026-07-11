using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Mvc;

namespace c05_SpaSmart.Pages.DanhMucDichVu
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<Models.DanhMucDichVu> DanhMucDichVus { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.DanhMucDichVus != null)
            {
                DanhMucDichVus = await _context.DanhMucDichVus.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var danhmuc = await _context.DanhMucDichVus.FindAsync(id);
            if (danhmuc != null)
            {
                _context.DanhMucDichVus.Remove(danhmuc);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
