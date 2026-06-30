using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Models
{
    public class KyThuatVien
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string HoTen { get; set; } = null!;

        [Required, MaxLength(100)]
        public string ChuyenMon { get; set; } = null!;

        [Required]
        public TrangThaiKTV TrangThaiKTV { get; set; } = TrangThaiKTV.SanSang;

        // Navigation properties
        public ICollection<LichHen>? LichHens { get; set; }
    }
}
