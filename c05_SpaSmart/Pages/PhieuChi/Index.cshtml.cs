using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.PhieuChi
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public IList<c05_SpaSmart.Models.PhieuChi> PhieuChis { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.PhieuChis != null)
            {
                PhieuChis = await _context.PhieuChis.OrderByDescending(x => x.NgayChi).ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostCreateAsync(string LyDoChi, string NguoiChi, decimal SoTien)
        {
            var pc = new c05_SpaSmart.Models.PhieuChi
            {
                LyDoChi = LyDoChi,
                NguoiChi = NguoiChi,
                SoTien = SoTien,
                NgayChi = DateTime.Now
            };
            _context.PhieuChis.Add(pc);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Đã lưu phiếu chi!";
            return RedirectToPage();
        }
    }
}
