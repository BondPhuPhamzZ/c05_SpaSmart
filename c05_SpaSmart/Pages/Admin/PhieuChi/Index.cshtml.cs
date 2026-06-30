using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace c05_SpaSmart.Pages.Admin.PhieuChi
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public IndexModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.PhieuChi> PhieuChiList { get;set; } = default!;
        public decimal TongChiTieu { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.PhieuChis != null)
            {
                PhieuChiList = await _context.PhieuChis
                    .OrderByDescending(p => p.NgayLap)
                    .ToListAsync();
                
                TongChiTieu = PhieuChiList.Sum(p => p.SoTien);
            }
        }
    }
}
