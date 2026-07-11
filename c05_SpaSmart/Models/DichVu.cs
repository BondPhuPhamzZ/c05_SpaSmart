using System;
using System.Collections.Generic;

namespace c05_SpaSmart.Models;

public partial class DichVu
{
    public int Id { get; set; }

    public int DanhMucId { get; set; }

    public string TenDichVu { get; set; } = null!;

    public int Loai { get; set; }

    public decimal GiaTien { get; set; }

    public decimal GiamGia { get; set; }

    public string? MoTa { get; set; }

    public string? HinhAnh { get; set; }

    public virtual ICollection<ChiTietPhieuTinhTien> ChiTietPhieuTinhTiens { get; set; } = new List<ChiTietPhieuTinhTien>();

    public virtual DanhMucDichVu DanhMuc { get; set; } = null!;
}
