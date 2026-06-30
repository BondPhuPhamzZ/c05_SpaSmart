using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.GoiDichVu
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public IndexModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.GoiDichVu> GoiDichVuList { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.GoiDichVus != null)
            {
                GoiDichVuList = await _context.GoiDichVus.ToListAsync();
            }
        }
    }
}
