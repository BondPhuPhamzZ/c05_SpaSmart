using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class GoiDichVu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string TenDichVu { get; set; } = null!;

        public string? MoTa { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaTien { get; set; }

        [Required]
        public int ThoiLuongPhut { get; set; }

        // Navigation properties
        public virtual ICollection<ChiTietLichHen> ChiTietLichHens { get; set; } = new List<ChiTietLichHen>();
        public virtual ICollection<DinhMucPhuLieu> DinhMucPhuLieus { get; set; } = new List<DinhMucPhuLieu>();
    }
}
