using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using c05_SpaSmart.Models.Enums;

namespace c05_SpaSmart.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required, MaxLength(255)]
        public string PasswordHash { get; set; } = null!;

        [Required, MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required, MaxLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        public UserRole Role { get; set; }

        public int DiemTichLuy { get; set; } = 0;

        // Navigation properties
        public ICollection<LichHen>? LichHens { get; set; }
        public ICollection<PhieuChi>? PhieuChis { get; set; }
    }
}
