using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;
using c05_SpaSmart.Models.Enums;
using BCrypt.Net;

namespace c05_SpaSmart.Data
{
    public class SpaSmartDbContext : DbContext
    {
        public SpaSmartDbContext(DbContextOptions<SpaSmartDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<KyThuatVien> KyThuatViens { get; set; }
        public DbSet<GoiDichVu> GoiDichVus { get; set; }
        public DbSet<PhuLieu> PhuLieus { get; set; }
        public DbSet<DinhMucPhuLieu> DinhMucPhuLieus { get; set; }
        public DbSet<LichHen> LichHens { get; set; }
        public DbSet<ChiTietLichHen> ChiTietLichHens { get; set; }
        public DbSet<PhieuChi> PhieuChis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Composite keys
            modelBuilder.Entity<DinhMucPhuLieu>()
                .HasKey(d => new { d.DichVuId, d.PhuLieuId });

            modelBuilder.Entity<ChiTietLichHen>()
                .HasKey(c => new { c.LichHenId, c.DichVuId });

            // Ensure no cascade delete conflicts for LichHen
            modelBuilder.Entity<LichHen>()
                .HasOne(l => l.User)
                .WithMany(u => u.LichHens)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LichHen>()
                .HasOne(l => l.KyThuatVien)
                .WithMany(k => k.LichHens)
                .HasForeignKey(l => l.KyThuatVienId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<PhieuChi>()
                .HasOne(p => p.User)
                .WithMany(u => u.PhieuChis)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
