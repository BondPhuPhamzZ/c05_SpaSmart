using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class PhuLieu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string TenPhuLieu { get; set; } = null!;

        [StringLength(100)]
        public string? HangSanXuat { get; set; }

        public int TonKho { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; }

        // Navigation properties
        public virtual ICollection<DinhMucPhuLieu> DinhMucPhuLieus { get; set; } = new List<DinhMucPhuLieu>();
    }
}
