using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.LichHen
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public IndexModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.LichHen> LichHenList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.LichHens != null)
            {
                LichHenList = await _context.LichHens
                .Include(l => l.KyThuatVien)
                .Include(l => l.User)
                .OrderByDescending(l => l.NgayGioDat)
                .ToListAsync();
            }
        }
    }
}
