using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace c05_SpaSmart.Pages.Admin.PhieuChi
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly c05_SpaSmart.Data.SpaSmartDbContext _context;

        public CreateModel(c05_SpaSmart.Data.SpaSmartDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public c05_SpaSmart.Models.PhieuChi PhieuChi { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PhieuChis == null || PhieuChi == null)
            {
                return Page();
            }

            if (PhieuChi.SoTien <= 0)
            {
                ModelState.AddModelError("PhieuChi.SoTien", "Số tiền chi không hợp lệ. Vui lòng nhập lại số tiền > 0 (MS08).");
                return Page();
            }

            // Gắn thông tin ẩn
            PhieuChi.NgayLap = DateTime.Now;

            // Tìm user ID của Admin đang đăng nhập
            var username = User.Identity?.Name;
            if(!string.IsNullOrEmpty(username))
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if(user != null)
                {
                    PhieuChi.UserId = user.Id;
                }
            }

            _context.PhieuChis.Add(PhieuChi);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Phiếu chi đã được ghi nhận thành công (MS07).";
            return RedirectToPage("./Index");
        }
    }
}
