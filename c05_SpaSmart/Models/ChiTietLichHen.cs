using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class ChiTietLichHen
    {
        public int LichHenId { get; set; }
        public LichHen? LichHen { get; set; }

        public int DichVuId { get; set; }
        public GoiDichVu? GoiDichVu { get; set; }

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal GiaTienLucDat { get; set; }
    }
}
