using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Models
{
    public class LichHen
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime NgayGioDat { get; set; }

        [Required]
        public TrangThaiLichHen TrangThai { get; set; } = TrangThaiLichHen.ChoXacNhan;

        public string? GhiChu { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TienGiamGia { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongThanhToan { get; set; } = 0;

        public DateTime? NgayLapHoaDon { get; set; }

        [Required]
        public int UserId { get; set; } // FK to Customer
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        public int? KyThuatVienId { get; set; } // FK to KTV
        
        [ForeignKey("KyThuatVienId")]
        public virtual KyThuatVien? KyThuatVien { get; set; }

        // Navigation properties
        public virtual ICollection<ChiTietLichHen> ChiTietLichHens { get; set; } = new List<ChiTietLichHen>();
    }
}
