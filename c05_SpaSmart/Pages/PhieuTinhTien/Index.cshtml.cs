using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Pages.PhieuTinhTien
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.PhieuTinhTien> PhieuTinhTiens { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PhieuTinhTiens != null)
            {
                PhieuTinhTiens = await _context.PhieuTinhTiens.OrderByDescending(x => x.NgayThanhToan).ToListAsync();
            }
        }
    }
}
