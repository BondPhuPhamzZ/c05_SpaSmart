using System;
using System.Collections.Generic;

namespace c05_SpaSmart.Models;

public partial class PhieuChi
{
    public int Id { get; set; }

    public string LyDoChi { get; set; } = null!;

    public decimal SoTien { get; set; }

    public DateTime NgayChi { get; set; }

    public string NguoiChi { get; set; } = null!;
}
