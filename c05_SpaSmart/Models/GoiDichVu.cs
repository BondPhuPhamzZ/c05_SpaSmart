using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class GoiDichVu
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string TenDichVu { get; set; } = null!;

        public string? MoTa { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal GiaTien { get; set; }

        [Required]
        public int ThoiLuongPhut { get; set; }

        // Navigation properties
        public ICollection<ChiTietLichHen>? ChiTietLichHens { get; set; }
        public ICollection<DanhMucPhuLieu>? DanhMucPhuLieus { get; set; }
    }
}
