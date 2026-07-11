using System;
using System.Collections.Generic;

namespace c05_SpaSmart.Models;

public partial class PhieuTinhTien
{
    public int Id { get; set; }

    public string TenKhachHang { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public string? TenKtv { get; set; }

    public DateTime NgayThanhToan { get; set; }

    public string? HinhThucThanhToan { get; set; }

    public decimal? TienKhachDua { get; set; }

    public decimal? TienThoiLai { get; set; }

    public decimal? TongGiamGia { get; set; }

    public decimal TongTien { get; set; }

    public string? TrangThai { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<ChiTietPhieuTinhTien> ChiTietPhieuTinhTiens { get; set; } = new List<ChiTietPhieuTinhTien>();
}
