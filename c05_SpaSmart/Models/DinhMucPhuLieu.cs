using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class DinhMucPhuLieu
    {
        // Composite Key will be configured in DbContext
        [Required]
        public int DichVuId { get; set; }
        
        [ForeignKey("DichVuId")]
        public virtual GoiDichVu GoiDichVu { get; set; } = null!;

        [Required]
        public int PhuLieuId { get; set; }

        [ForeignKey("PhuLieuId")]
        public virtual PhuLieu PhuLieu { get; set; } = null!;

        [Required]
        public int SoLuongTieuHao { get; set; }
    }
}
