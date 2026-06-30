using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Models
{
    public class LichHen
    {
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

        // Foreign Keys
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }

        public int? KyThuatVienId { get; set; }
        [ForeignKey("KyThuatVienId")]
        public KyThuatVien? KyThuatVien { get; set; }

        // Navigation properties
        public ICollection<ChiTietLichHen>? ChiTietLichHens { get; set; }
    }
}
