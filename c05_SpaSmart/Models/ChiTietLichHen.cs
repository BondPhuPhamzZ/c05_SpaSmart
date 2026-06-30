using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class ChiTietLichHen
    {
        [Required]
        public int LichHenId { get; set; }

        [ForeignKey("LichHenId")]
        public virtual LichHen LichHen { get; set; } = null!;

        [Required]
        public int DichVuId { get; set; }

        [ForeignKey("DichVuId")]
        public virtual GoiDichVu GoiDichVu { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaTienLucDat { get; set; }
    }
}
