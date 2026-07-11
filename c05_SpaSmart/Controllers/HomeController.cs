using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly C05SpaSmartContext _context;

        public HomeController(C05SpaSmartContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var dichVus = await _context.DichVus
                .Include(d => d.DanhMuc)
                .OrderBy(d => d.Loai)
                .ThenBy(d => d.TenDichVu)
                .ToListAsync();

            return View(dichVus);
        }
    }
}
