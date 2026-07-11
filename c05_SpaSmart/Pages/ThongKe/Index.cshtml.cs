using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using System.Linq;
using System.Threading.Tasks;

namespace c05_SpaSmart.Pages.ThongKe
{
    public class IndexModel : PageModel
    {
        private readonly C05SpaSmartContext _context;

        public IndexModel(C05SpaSmartContext context)
        {
            _context = context;
        }

        public decimal TongDoanhThu { get; set; }
        public decimal TongChiPhi { get; set; }
        public decimal LoiNhuan { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.PhieuTinhTiens != null)
            {
                TongDoanhThu = await _context.PhieuTinhTiens.SumAsync(x => x.TongTien);
            }

            if (_context.PhieuChis != null)
            {
                TongChiPhi = await _context.PhieuChis.SumAsync(x => x.SoTien);
            }

            LoiNhuan = TongDoanhThu - TongChiPhi;
        }
    }
}
