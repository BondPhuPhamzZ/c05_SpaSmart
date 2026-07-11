using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;

namespace c05_SpaSmart
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Add DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<C05SpaSmartContext>(options =>
                options.UseSqlServer(connectionString));

            // 2. Add Razor Pages
            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapRazorPages();

            app.Run();
        }
    }
}
