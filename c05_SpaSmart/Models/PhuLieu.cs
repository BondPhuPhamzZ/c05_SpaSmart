using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class PhuLieu
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string TenPhuLieu { get; set; } = null!;

        [Required, MaxLength(100)]
        public string HangSanXuat { get; set; } = null!;

        public int TonKho { get; set; } = 0;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        // Navigation properties
        public ICollection<DanhMucPhuLieu>? DanhMucPhuLieus { get; set; }
    }
}
