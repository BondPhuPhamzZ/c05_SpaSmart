using Microsoft.EntityFrameworkCore;
using c05_SpaSmart.Models;

namespace c05_SpaSmart.Data
{
    public class SpaSmartDbContext : DbContext
    {
        public SpaSmartDbContext(DbContextOptions<SpaSmartDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<KyThuatVien> KyThuatViens { get; set; } = null!;
        public DbSet<GoiDichVu> GoiDichVus { get; set; } = null!;
        public DbSet<PhuLieu> PhuLieus { get; set; } = null!;
        public DbSet<LichHen> LichHens { get; set; } = null!;
        public DbSet<ChiTietLichHen> ChiTietLichHens { get; set; } = null!;
        public DbSet<DanhMucPhuLieu> DanhMucPhuLieus { get; set; } = null!;
        public DbSet<PhieuChi> PhieuChis { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình Khóa chính kép cho ChiTietLichHen
            modelBuilder.Entity<ChiTietLichHen>()
                .HasKey(c => new { c.LichHenId, c.DichVuId });

            modelBuilder.Entity<ChiTietLichHen>()
                .HasOne(c => c.LichHen)
                .WithMany(l => l.ChiTietLichHens)
                .HasForeignKey(c => c.LichHenId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChiTietLichHen>()
                .HasOne(c => c.GoiDichVu)
                .WithMany(g => g.ChiTietLichHens)
                .HasForeignKey(c => c.DichVuId)
                .OnDelete(DeleteBehavior.Restrict);

            // Cấu hình Khóa chính kép cho DanhMucPhuLieu
            modelBuilder.Entity<DanhMucPhuLieu>()
                .HasKey(d => new { d.DichVuId, d.PhuLieuId });

            modelBuilder.Entity<DanhMucPhuLieu>()
                .HasOne(d => d.GoiDichVu)
                .WithMany(g => g.DanhMucPhuLieus)
                .HasForeignKey(d => d.DichVuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DanhMucPhuLieu>()
                .HasOne(d => d.PhuLieu)
                .WithMany(p => p.DanhMucPhuLieus)
                .HasForeignKey(d => d.PhuLieuId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ràng buộc Khóa ngoại khác
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
