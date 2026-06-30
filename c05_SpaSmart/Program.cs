using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Data;
using c05_SpaSmart.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace c05_SpaSmart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<SpaSmartDbContext>(options =>
                options.UseSqlServer(connectionString));

            // 2. Register Custom Services (Security)
            builder.Services.AddSingleton<SecurityService>();

            // 3. Configure Authentication (Cookie)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Login";
                    options.LogoutPath = "/Auth/Logout";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    options.SlidingExpiration = true;
                });

            // 4. Add MVC (for Customer) and Razor Pages (for Admin)
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Authentication & Authorization MUST be between UseRouting and MapControllers
            app.UseAuthentication();
            app.UseAuthorization();

            // Run DbSeeder to initialize sample data
            SpaSmartDbSeeder.Seed(app);

            // Map MVC Controllers (Customer routes)
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Map Razor Pages (Admin routes)
            app.MapRazorPages();

            app.Run();
        }
    }
}
