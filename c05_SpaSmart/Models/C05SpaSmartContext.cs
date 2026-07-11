using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace c05_SpaSmart.Models;

public partial class C05SpaSmartContext : DbContext
{
    public C05SpaSmartContext()
    {
    }

    public C05SpaSmartContext(DbContextOptions<C05SpaSmartContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChiTietPhieuTinhTien> ChiTietPhieuTinhTiens { get; set; }

    public virtual DbSet<DanhMucDichVu> DanhMucDichVus { get; set; }

    public virtual DbSet<DichVu> DichVus { get; set; }

    public virtual DbSet<PhieuChi> PhieuChis { get; set; }

    public virtual DbSet<PhieuTinhTien> PhieuTinhTiens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=C05_SpaSmart;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChiTietPhieuTinhTien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChiTietP__3214EC075727854B");

            entity.ToTable("ChiTietPhieuTinhTien");

            entity.Property(e => e.DonGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SoLuong).HasDefaultValue(1);

            entity.HasOne(d => d.DichVu).WithMany(p => p.ChiTietPhieuTinhTiens)
                .HasForeignKey(d => d.DichVuId)
                .HasConstraintName("FK__ChiTietPh__DichV__46E78A0C");

            entity.HasOne(d => d.PhieuTinhTien).WithMany(p => p.ChiTietPhieuTinhTiens)
                .HasForeignKey(d => d.PhieuTinhTienId)
                .HasConstraintName("FK__ChiTietPh__Phieu__45F365D3");
        });

        modelBuilder.Entity<DanhMucDichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DanhMucD__3214EC0777F44F19");

            entity.ToTable("DanhMucDichVu");

            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenDanhMuc).HasMaxLength(100);
        });

        modelBuilder.Entity<DichVu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DichVu__3214EC0798130355");

            entity.ToTable("DichVu");

            entity.Property(e => e.GiaTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GiamGia).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TenDichVu).HasMaxLength(255);

            entity.HasOne(d => d.DanhMuc).WithMany(p => p.DichVus)
                .HasForeignKey(d => d.DanhMucId)
                .HasConstraintName("FK__DichVu__DanhMucI__3A81B327");
        });

        modelBuilder.Entity<PhieuChi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuChi__3214EC07A1B6237C");

            entity.ToTable("PhieuChi");

            entity.Property(e => e.NgayChi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NguoiChi).HasMaxLength(255);
            entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<PhieuTinhTien>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PhieuTin__3214EC072852EBE5");

            entity.ToTable("PhieuTinhTien");

            entity.Property(e => e.HinhThucThanhToan).HasMaxLength(50);
            entity.Property(e => e.NgayThanhToan)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenKhachHang).HasMaxLength(255);
            entity.Property(e => e.TenKtv)
                .HasMaxLength(255)
                .HasColumnName("TenKTV");
            entity.Property(e => e.TienKhachDua)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TienThoiLai)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TongGiamGia)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TongTien).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Hoàn tất");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
