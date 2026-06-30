using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class PhieuChi
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string LyDoChi { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [StringLength(255)]
        public string? AnhChungTu { get; set; }

        [Required]
        public DateTime NgayLap { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}
