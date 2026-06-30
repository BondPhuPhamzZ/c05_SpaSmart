using System.ComponentModel.DataAnnotations;

namespace c05_SpaSmart.Models
{
    public class DanhMucPhuLieu
    {
        public int DichVuId { get; set; }
        public GoiDichVu? GoiDichVu { get; set; }

        public int PhuLieuId { get; set; }
        public PhuLieu? PhuLieu { get; set; }

        [Required]
        public int SoLuongTieuHao { get; set; }
    }
}
