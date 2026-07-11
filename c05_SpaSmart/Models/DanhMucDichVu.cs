using System;
using System.Collections.Generic;

namespace c05_SpaSmart.Models;

public partial class DanhMucDichVu
{
    public int Id { get; set; }

    public string TenDanhMuc { get; set; } = null!;

    public string? MoTa { get; set; }

    public virtual ICollection<DichVu> DichVus { get; set; } = new List<DichVu>();
}
