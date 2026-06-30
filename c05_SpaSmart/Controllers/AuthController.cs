using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;
using c05_SpaSmart.Models.ViewModels;
using c05_SpaSmart.Services;

namespace c05_SpaSmart.Controllers
{
    public class AuthController : Controller
    {
        private readonly SpaSmartDbContext _context;
        private readonly SecurityService _securityService;

        public AuthController(SpaSmartDbContext context, SecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                
                if (user != null && _securityService.VerifyPassword(model.Password, user.PasswordHash))
                {
                    // Tạo Cookie Auth
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = false };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    // Redirect dựa trên Role
                    if (user.Role == UserRole.Admin)
                    {
                        return Redirect("/Admin/LichHen"); // Razor Page của Admin
                    }

                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra trùng username
                var exists = await _context.Users.AnyAsync(u => u.Username == model.Username);
                if (exists)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    PasswordHash = _securityService.HashPassword(model.Password),
                    FullName = model.FullName,
                    PhoneNumber = model.PhoneNumber,
                    Role = UserRole.Customer,
                    DiemTichLuy = 0
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction(nameof(Login));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
