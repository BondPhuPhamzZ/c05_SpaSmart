using System;
using System.Collections.Generic;

namespace c05_SpaSmart.Models;

public partial class ChiTietPhieuTinhTien
{
    public int Id { get; set; }

    public int PhieuTinhTienId { get; set; }

    public int DichVuId { get; set; }

    public int SoLuong { get; set; }

    public decimal DonGia { get; set; }

    public virtual DichVu DichVu { get; set; } = null!;

    public virtual PhieuTinhTien PhieuTinhTien { get; set; } = null!;
}
