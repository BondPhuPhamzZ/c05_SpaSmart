using System.ComponentModel.DataAnnotations;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Models
{
    public class KyThuatVien
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string ChuyenMon { get; set; } = null!;

        [Required]
        public TrangThaiKTV TrangThaiKTV { get; set; } = TrangThaiKTV.SanSang;

        // Navigation properties
        public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();
    }
}
