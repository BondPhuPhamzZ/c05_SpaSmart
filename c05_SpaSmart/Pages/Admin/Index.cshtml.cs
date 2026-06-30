using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Data;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly SpaSmartDbContext _context;

        public IndexModel(SpaSmartDbContext context)
        {
            _context = context;
        }

        public decimal TongDoanhThu { get; set; }
        public int SoDonDaHoanThanh { get; set; }
        public decimal TongChiTieu { get; set; }
        public decimal LoiNhuan => TongDoanhThu - TongChiTieu;

        public void OnGet()
        {
            if (_context.LichHens != null)
            {
                var lichHensDaThanhToan = _context.LichHens.Where(l => l.TrangThai == Models.Enums.TrangThaiLichHen.DaThanhToan).ToList();
                TongDoanhThu = lichHensDaThanhToan.Sum(h => h.TongThanhToan);
                SoDonDaHoanThanh = lichHensDaThanhToan.Count;
            }
            if (_context.PhieuChis != null)
            {
                TongChiTieu = _context.PhieuChis.Sum(p => p.SoTien);
            }
        }
    }
}
