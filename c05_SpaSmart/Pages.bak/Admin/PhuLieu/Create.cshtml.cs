using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using Microsoft.AspNetCore.Authorization;

namespace c05_SpaSmart.Pages.Admin.PhuLieu
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
        public c05_SpaSmart.Models.PhuLieu PhuLieu { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.PhuLieus == null || PhuLieu == null)
            {
                return Page();
            }

            if (PhuLieu.DonGia < 0)
            {
                ModelState.AddModelError("PhuLieu.DonGia", "Giá phụ liệu không được là số âm. Vui lòng kiểm tra lại"); // MS10
                return Page();
            }

            _context.PhuLieus.Add(PhuLieu);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thêm phụ liệu mới thành công!";
            return RedirectToPage("./Index");
        }
    }
}
