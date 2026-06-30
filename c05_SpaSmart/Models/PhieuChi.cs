using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c05_SpaSmart.Models
{
    public class PhieuChi
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string LyDoChi { get; set; } = null!;

        [Required, Column(TypeName = "decimal(18,2)")]
        public decimal SoTien { get; set; }

        [MaxLength(255)]
        public string? AnhChungTu { get; set; }

        [Required]
        public DateTime NgayLap { get; set; } = DateTime.Now;

        // Foreign Keys
        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
