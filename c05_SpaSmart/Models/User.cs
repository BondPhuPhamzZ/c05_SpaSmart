using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }

        public int DiemTichLuy { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<LichHen> LichHens { get; set; } = new List<LichHen>();
        public virtual ICollection<PhieuChi> PhieuChis { get; set; } = new List<PhieuChi>();
    }
}
