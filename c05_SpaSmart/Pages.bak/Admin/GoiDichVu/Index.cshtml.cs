using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;

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

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var goidichvu = await _context.GoiDichVus.FindAsync(id);
            if (goidichvu != null)
            {
                _context.GoiDichVus.Remove(goidichvu);
                try
                {
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Đã xóa dịch vụ thành công.";
                }
                catch (DbUpdateException)
                {
                    TempData["ErrorMessage"] = "Không thể xóa dịch vụ này vì đã được sử dụng trong lịch hẹn hoặc có định mức phụ liệu.";
                }
            }
            return RedirectToPage("./Index");
        }
    }
}
