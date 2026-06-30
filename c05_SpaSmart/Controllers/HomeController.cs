using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using System.Threading.Tasks;

namespace c05_SpaSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpaSmartDbContext _context;

        public HomeController(SpaSmartDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // UC02: Khách hàng xem thông tin dịch vụ Spa
            var services = await _context.GoiDichVus.ToListAsync();
            return View(services);
        }
    }
}
