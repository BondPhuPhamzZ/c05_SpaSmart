using System.ComponentModel.DataAnnotations;

namespace c05_SpaSmart.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập từ 3 đến 50 ký tự")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "Mật khẩu tối thiểu 6 ký tự")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        [MaxLength(15)]
        public string PhoneNumber { get; set; } = null!;
    }
}
